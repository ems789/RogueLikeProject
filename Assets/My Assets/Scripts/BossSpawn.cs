using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject[] bosses;

    // Start is called before the first frame update
    void Start()
    {
        GameObject toInstantiate = bosses[Random.Range(0, bosses.Length)];
        GameObject instance = Instantiate(toInstantiate);
        instance.transform.position = transform.position;
    }

}
