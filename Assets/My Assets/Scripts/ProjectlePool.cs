using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectlePool : MonoBehaviour
{

    public GameObject[] projectile;
    private static ObjectPool[] projectilePool;

    private int projectileCnt = 10;
    private int poolCnt;

    private void Start()
    {
        poolCnt = projectile.Length;

        projectilePool = new ObjectPool[poolCnt];
        for (int i = 0; i < poolCnt; i++)
            projectilePool[i] = new ObjectPool();

        for(int i=0; i<poolCnt; i++)
        {
            projectilePool[i].InitPool(projectile[i], projectileCnt);
        }                     
    }
}
