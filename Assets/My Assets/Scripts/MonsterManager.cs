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
    private double meleePoolMax = 0, rangerPoolMax = 0;

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
        
        
        meleePoolMax = maxPool * 0.7f;
        rangerPoolMax = maxPool * 0.3f;

        monsterPool[0].InitPool(monster[0], (int)meleePoolMax);
        monsterPool[1].InitPool(monster[1], (int)rangerPoolMax);
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
                        if (monsterCnt <= 5) // 테스트용 몬스터 제한
                        {
                            monsterCnt++;

                            float chance = Random.Range(0f, 1f);

                            if (chance >= 0 && chance <= 0.3f)
                                monsterType = Monster.MonsterType.RANGER;
                            else if (chance > 0.3 && chance < 1.0f)
                                monsterType = Monster.MonsterType.MELEE;

                            monsterPool[(int)monsterType].GetObject(x, y);
                        }
                    }
                }
            }
        }
        Debug.Log("몬스터 수 : " + monsterCnt);
    }
}
