using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator attackEffect;
    private Animator temp;

    private int atk;
    private bool isAttack = false;
    private int enemyHP;

    //test
    Vector2 lookDirection = new Vector2(0, 0);

    private void Start()
    {
        atk = GetComponentInParent<Player>().atk;
        Debug.Log(atk);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        isAttack = GetComponentInParent<PlayerController>().getIsAttack();
        if (other.gameObject.tag == "Enemy" && isAttack)
        {
            if (isAttack)
            {
                temp = Instantiate(attackEffect, other.transform.position, Quaternion.identity);
                temp.SetFloat("Type", 3.0f);
                temp.SetTrigger("Trigger");
                other.gameObject.GetComponent<Monster>().TakeDamage(atk);

            }
        }
    }
}
