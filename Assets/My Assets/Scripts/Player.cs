using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerParts;

    public int hp, currentHP;
    public int mp, currentMP;

    public int attackDamage;
    public int def;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        currentHP = hp;

        for (int i = 0; i < transform.childCount; i++) 
        {
            if (transform.GetChild(i).tag == "Player") // 자식 중 플레이어에 해당하는 오브젝트만
                playerParts++;
        }
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
            // 게임 오버 처리 추가
        }

        for (int i = 0; i <= playerParts; i++) 
        {
            StartCoroutine(HitCoroutine(i));
        }
    }

    // 피격시 깜빡임 처리
    IEnumerator HitCoroutine(int index)
    {
        SpriteRenderer[] childrenSprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color newColor;
        byte alphaColor = 240;

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, alphaColor);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, alphaColor);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, alphaColor);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, alphaColor);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 0, 0, alphaColor);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);

        newColor = childrenSprite[index].color;
        newColor = new Color32(255, 255, 255, 255);
        childrenSprite[index].color = newColor;
        yield return new WaitForSeconds(0.1f);
    }
}
