using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private float range;    
    private float liveTime;
    private float timeCheck = 0;

    private void Update()
    {
        timeCheck += Time.deltaTime;
        if(timeCheck > liveTime)
        {
            SetProject();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(damage);
            SetProject();
        }
        else if (other.transform.tag == "Wall")
        {
            SetProject();
        }
    }

    private void SetProject()
    {
        timeCheck = 0;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); // 재활용할것이므로 되돌리기 전에 바라보는 방향 초기화
        gameObject.SetActive(false);
    }

    public void SetProject(int _damage, float _range, float _liveTime)
    {
        damage = _damage;
        range = _range;
        liveTime = _liveTime;
    }

    // 사거리 적용 추가
}
