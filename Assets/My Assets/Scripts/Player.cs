using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int hp;
    public int currentHP;
    public int mp;
    public int currentMP;

    public int attackDamage;
    public int def;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)        
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        currentHP = hp;
    }

    public void TakeDamage(int enemyAttackDamage)
    {
        int damage;

        if (def >= enemyAttackDamage)
            damage = 1;
        else
            damage = enemyAttackDamage - def;
       
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Debug.Log("체력 0 미만, 게임오버");
            // 게임 오버 처리
        }

        // 피격 처리(연결된 자식들)
        for (int i = 0; i < transform.childCount -3; i++) // 임시, 나중에 태그로 플레이어 검사해서 그것만 알파처리
        {
            StartCoroutine(HitCoroutine(i));
        }
    }

    // 피격시 깜빡임 처리
    IEnumerator HitCoroutine(int index)
    {
        SpriteRenderer[] childrenSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color newColor;

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, 230);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, 230);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, 230);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, 230);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, 230);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, 255);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);



    }




}
