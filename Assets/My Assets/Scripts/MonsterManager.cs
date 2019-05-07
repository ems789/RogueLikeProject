using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public GameObject[] monster;

    public float chanceToCreateMonster = 0.01f;
    Monster.MonsterType monsterType;
   
    
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
                        float chance = Random.Range(0f, 1f);

                        if (chance >= 0 && chance <= 0.3f)
                            monsterType = Monster.MonsterType.RANGER;
                        else if (chance > 0.3 && chance < 1.0f)
                            monsterType = Monster.MonsterType.MELEE;
                        
                        GameObject monsterInstantiate = Instantiate(monster[(int)monsterType],
                            new Vector3(x, y, 0f), Quaternion.identity);

                        if (monsterInstantiate.GetComponent<Monster>().PlayerInScope())
                        {
                            Debug.Log(monsterInstantiate.GetComponent<Monster>().PlayerInScope());
                            Destroy(monsterInstantiate);
                        }
                        else
                            monsterInstantiate.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }
        
    }
}
