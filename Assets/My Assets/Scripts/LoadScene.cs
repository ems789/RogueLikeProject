using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.SceneMove();
    }
}
