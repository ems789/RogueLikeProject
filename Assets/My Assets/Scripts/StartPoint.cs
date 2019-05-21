using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    void Start()
    {
        Transform player = FindObjectOfType<Player>().transform;
        player.position = transform.position;
    }
}
