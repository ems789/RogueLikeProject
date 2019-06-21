using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance = null;

    public GameObject[] monster;
    private ObjectPool[] monsterPool;

    private int poolCnt;
    public int monsterCnt;

    public float chanceToCreateMonster = 0.01f;

    private Monster.MonsterType monsterType;
    
    private int maxPool = DungeonManager.width + DungeonManager.height;
    private double poolMax = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        poolCnt = monster.Length;
        monsterPool = new ObjectPool[poolCnt];
        for(int i=0; i<poolCnt; i++)
            monsterPool[i] = new ObjectPool();
                
        poolMax = 10;

        monsterPool[0].InitPool(monster[0], (int)poolMax);
        monsterPool[1].InitPool(monster[1], (int)poolMax);
        monsterPool[2].InitPool(monster[2], (int)poolMax);
    }

    public void MonsterSetting()
    {
        for (int x = 0; x < DungeonManager.width; x++)
        {
            for(int y=0; y < DungeonManager.height; y++)
            {
                if(DungeonManager.cellmap[x,y] == false)
                {
                    if(Random.Range(0f, 1f) < chanceToCreateMonster)
                    {
                        if (monsterCnt < 12) // 몬스터 제한
                        {
                            monsterCnt++;

                            float chance = Random.Range(0, 3);

                            if (chance == 0)
                                monsterType = Monster.MonsterType.RANGER;
                            else if (chance == 1)
                                monsterType = Monster.MonsterType.MELEE;
                            else if (chance == 2)
                                monsterType = Monster.MonsterType.DASH;

                            monsterPool[(int)monsterType].GetObject(x, y);
                        }
                    }
                }
            }
        }
    }
}
