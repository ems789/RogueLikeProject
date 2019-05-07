using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monster;

    public float chanceToCreateMonster = 0.01f;
    private Monster.MonsterType monsterType;

    List<List<GameObject>> monsterPool = new List<List<GameObject>>();

    private int maxPool = DungeonManager.width + DungeonManager.height;
    private double meleePoolMax = 0, rangerPoolMax = 0;

    private void Start()
    {
        meleePoolMax = maxPool * 0.7f;
        rangerPoolMax = maxPool * 0.3f;

        PoolInit(monsterPool[(int)Monster.MonsterType.MELEE], meleePoolMax, Monster.MonsterType.MELEE);
        PoolInit(monsterPool[(int)Monster.MonsterType.RANGER], rangerPoolMax, Monster.MonsterType.RANGER);
    }

    private void PoolInit(List<GameObject> monsterList, double max, Monster.MonsterType type)
    {
        int i = 0;
        for (i = 0; i < max; i++)
        {
            GameObject obj = Instantiate(monster[(int)type]);
            obj.name += i;
            obj.SetActive(false);

            monsterList.Add(obj);
        }
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

                        GameObject monsterObj = monsterPool[(int)monsterType].Find(item => item.activeSelf == false);                       

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
