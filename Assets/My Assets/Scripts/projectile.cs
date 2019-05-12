using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float range;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player.instance.TakeDamage(damage);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); // 재활용할것이므로 되돌리기 전에 바라보는 방향 초기화
            gameObject.SetActive(false);
        }
        else if (other.transform.tag == "Wall")
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.SetActive(false);
        }               
    }

    public void InitProjectile(int _damage, float _range)
    {
        damage = _damage;
        range = _range;
    }

    // 사거리 적용 추가
}
