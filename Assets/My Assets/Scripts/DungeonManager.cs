using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] chest;
    public GameObject[] monster;

    private Transform boardHolder;
    private Transform playerTrans;

    public int numberOfSteps = 2;
    public int birthLimit = 4, deathLimit = 3;

    public float chanceToStartAlive = 0.4f; // 벽이 생성될 확률
    public float chanceToCreateChest = 0.5f;
    public float chanceToCreateMonster = 0.01f;

    public static int width = 35, height = 35;
    private int tileCount = 0;

    public static bool[,] cellmap = new bool[width, height]; // true는 벽 false는 타일
    private bool[,] checkedTile = new bool[width, height];
    private double minimumTile = width * height / 2.5; // 최소 깔려야 하는 타일의 수
    

    public void SetupDungeon()
    {
        boardHolder = new GameObject("Board").transform;
        playerTrans = GameObject.FindWithTag("Player").transform;
        MapBorderFill();

        do
        {
            InitializeMap();
            tileCount = 0;

            cellmap = GenerateMap();
            MoveOverlappedPlayer();

            TileCheck((int)playerTrans.position.x, (int)playerTrans.position.y);
        } while (tileCount <= minimumTile);
        
        drawMapTiles(cellmap);        
        ObjectSetting(cellmap);
    }

    public void InitializeMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cellmap[x, y] = false;
                checkedTile[x, y] = false;
            }
        }
    }

    // bool로 맵 초기화
    public bool[,] CreateCell(bool[,] map)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (Random.Range(0f, 1f) < chanceToStartAlive) // chanceToAlive의 확률로 true(벽)
                {
                    map[x, y] = true;
                }
                else
                {
                    map[x, y] = false;
                }

            }
        }
        return map;
    }

    // 생성된 맵대로 월드에 타일을 깐다
    public void drawMapTiles(bool[,] map)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if (map[x, y])
                {
                    toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public bool[,] doSimulationStep(bool[,] oldMap)
    {
        bool[,] newMap = new bool[width,height];
        for(int x=0; x<oldMap.GetLength(0); x++)
        {
            for(int y=0; y<oldMap.GetLength(1); y++)
            {
                int nbs = countAliveNeighbours(oldMap, x, y);
                if(oldMap[x,y])
                    // 세포가 살아있는 경우
                {
                    if(nbs < deathLimit)
                        // 이웃이 너무 적은 경우 죽인다
                    {
                        newMap[x, y] = false;
                    }
                    else
                    {
                        newMap[x, y] = true;
                    }
                }
                else
                    // 세포가 죽은 경우
                {
                    if(nbs > birthLimit)
                        // 이웃이 많으면 살린다
                    {
                        newMap[x, y] = true;
                    }
                    else
                    {
                        newMap[x, y] = false;
                    }
                }
            }
        }
        return newMap;
    }

    public bool[,] GenerateMap()
    {
        bool[,] cellmap = new bool[width, height];
        cellmap = CreateCell(cellmap);
        // 시뮬레이션
        for (int i = 0; i < numberOfSteps; i++)
        {
            cellmap = doSimulationStep(cellmap);
        }

        return cellmap;
    }

    public int countAliveNeighbours(bool[,] map, int x, int y)
        // 살아 있는 이웃의 셀 수(x,y)를 반환
    {
        int count = 0;
        for(int i=-1; i<2; i++)
        {
            for(int j=-1; j<2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;
                if (i == 0 && j == 0)
                // 중간지점(자기 자신)에는 아무 처리를 하지 않는다.
                {
                }
                else if (neighbour_x < 0 || neighbour_y < 0
                   || neighbour_x >= map.GetLength(0) || neighbour_y >= map.GetLength(1)) 
                   // 지도를 벗어나는 경우 
                {
                    count++; // 실제가 아닌 그리드를 검사 시 이웃으로 계싼한다
                }
                else if(map[neighbour_x,neighbour_y])
                    // 그렇지 않으면 살아있는 셀 수를 점검
                {
                    count++;
                }

            }
        }

        return count;
    }

    // 맵 테두리 채우기
    public void MapBorderFill()
    {
        GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
        int x = -1, y = -1;
    
        for (x = -1; x < width; x++) 
        {
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        }

        for (y = -1; y < height; y++)
        {
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        } 

        for(; x > -1; x--)
        {
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        }

        for(; y > -1; y--)
        {
            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
            instance.transform.SetParent(boardHolder);
        }
    }


    // 타일, 몬스터를 던전에 셋팅
    public void ObjectSetting(bool[,] map)
    {
        GameObject toInstantiate = chest[Random.Range(0, chest.Length)];

        int treasureHiddenLimit = 5;
        for(int x=0; x<width; x++)
        {
            for(int y=0; y<height; y++)
            {
                if(map[x,y] == false)
                {
                    int nbs = countAliveNeighbours(map, x, y);
                    if(nbs >= treasureHiddenLimit)
                    {
                        if (Random.Range(0f, 1f) < chanceToStartAlive)
                        {
                            GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);
                            instance.transform.SetParent(boardHolder);
                        }
                    }

                    if(Random.Range(0f, 1f) < chanceToCreateMonster)
                    {
                        GameObject monsterInstantiate = Instantiate(monster[Random.Range(0, monster.Length)], new Vector3(x, y, 0f), Quaternion.identity);

                        if (monsterInstantiate.GetComponent<Monster>().PlayerInScope())
                        {
                            Debug.Log(monsterInstantiate.GetComponent<Monster>().PlayerInScope());
                            Destroy(monsterInstantiate);
                        }
                        else
                            monsterInstantiate.transform.SetParent(boardHolder);

                    }
                }
            }
        }
    }

    // 플레이어와 벽이 겹치는 경우 플레이어의 위치를 조정
    public void MoveOverlappedPlayer()     
    {
        int x = 0, y = 0;

        while (cellmap[x, y] == true)  // 돌타일과 플레이어 위치가 겹칠경우
        {
            y = x+1;
            x = 0;

            while (x < y) // → 방향 탐색
            {
                x++;
                if (cellmap[x, y] == false)
                {
                    playerTrans.position = new Vector3(x, y);
                    return;
                }
            }

            while(y > 0) // ↓ 방향 탐색
            {
                y--;
                if (cellmap[x, y] == false)
                {
                    playerTrans.position = new Vector3(x, y);
                    return;
                }
            }
        }
    }

    void TileCheck(int x, int y)
    {
        if (x >= width || y >= height || x < 0 || y < 0)
            return;
        else if (cellmap[x, y] == true) // 벽이면 리턴
            return;
        else if (checkedTile[x,y] == true)
            return;
        else
        {
            checkedTile[x, y] = true;
            tileCount++;
            TileCheck(x + 1, y);
            TileCheck(x, y + 1);
            TileCheck(x - 1, y);
            TileCheck(x, y - 1);
        }
    }
}
