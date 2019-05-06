﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator attackEffect;
    private Animator temp;

    public int weaponDamage;
    private bool isAttack = false;
    private int enemyHP;
    

    private void Start()
    {
        weaponDamage = GetComponentInParent<Player>().attackDamage + weaponDamage;   
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
                other.gameObject.GetComponent<Monster>().TakeDamage(weaponDamage);

            }
        }
    }
}
