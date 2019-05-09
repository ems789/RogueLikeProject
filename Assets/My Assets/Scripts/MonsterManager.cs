using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monster;

    private ObjectPool[] monsterPool;

    private int monsterPoolCnt = (int)Monster.MonsterType.NumberOfTypes;

    public float chanceToCreateMonster = 0.01f;
    private Monster.MonsterType monsterType;
    
    private int maxPool = DungeonManager.width + DungeonManager.height;
    private double meleePoolMax = 0, rangerPoolMax = 0;

    private void Start()
    {
        monsterPool = new ObjectPool[monsterPoolCnt];
        for(int i=0; i<monsterPoolCnt; i++)
            monsterPool[i] = new ObjectPool();
        
        
        meleePoolMax = maxPool * 0.7f;
        rangerPoolMax = maxPool * 0.3f;

        monsterPool[0].InitPool(monster[0], (int)meleePoolMax); // 동적배열을 초기화 하지 않아서?
        monsterPool[1].InitPool(monster[1], (int)rangerPoolMax);
    }

    public void MonsterSetting()
    {
        int mel=0, rag=0;
        for (int x = 0; x < DungeonManager.width; x++)
        {
            for(int y=0; y < DungeonManager.height; y++)
            {
                if(DungeonManager.cellmap[x,y] == false)
                {
                    if(Random.Range(0f, 1f) < chanceToCreateMonster)
                    {
                        float chance = Random.Range(0f, 1f);

                        if (chance >= 0 && chance <= 0.3f)
                        {
                            monsterType = Monster.MonsterType.RANGER;
                            rag++;
                        }
                        else if (chance > 0.3 && chance < 1.0f)
                        {
                            monsterType = Monster.MonsterType.MELEE;
                            mel++;
                        }

                        GameObject monsterObj = Instantiate(monster[(int)monsterType], new Vector3(x, y, 0f), Quaternion.identity);

                        if (monsterObj == null)
                            return;

                        monsterObj.transform.position = new Vector3(x, y, 0f);
                        monsterObj.SetActive(true);

                        if (monsterObj.GetComponent<Monster>().PlayerInScope())
                        {
                            Debug.Log(monsterObj.GetComponent<Monster>().PlayerInScope());
                            monsterObj.SetActive(false);
                        }
                        else
                            monsterObj.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }

        Debug.Log(mel + " " + rag);
        
    }
}
