using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public int damage;

    public float waitingTime;
    private float chkTime = 0;

    private BoxCollider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        chkTime += Time.deltaTime;
        while(chkTime > waitingTime)
        {
            myCollider.enabled = true;
            chkTime = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.transform.tag == "Player")
        {
            Debug.Log("충돌");
            collision.SendMessage("TakeDamage", damage);
        }

    }
}
