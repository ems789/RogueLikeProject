using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    int damage;

    private void Start()
    {
        damage = GetComponentInParent<Monster>().attackDamage;
        transform.parent = null;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.transform.tag == "Wall")
            Destroy(gameObject);
    }
}
