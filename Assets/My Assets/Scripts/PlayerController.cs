using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 4;

    Rigidbody2D rigidbody2d;

    Animator animator;
    Vector2 lookDirection = new Vector2(0, 0);
    Vector2 AbsLookDirection = new Vector2(0, 0);

    private bool isAttack;
 
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Walk", false);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        // 캐릭터 이동
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        // 두 실수 비교(근사치, 키 입력이 0이 아니면 움직이는 방향을 구함)
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            animator.SetBool("Walk", true);
        }

        Vector2 position = rigidbody2d.position;

        position = position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);

        // 좌표값을 절대값으로 변환
        AbsLookDirection.x = Mathf.Abs(lookDirection.x);
        AbsLookDirection.y = Mathf.Abs(lookDirection.y);

        // 캐릭터 방향 설정
        if (AbsLookDirection.x > AbsLookDirection.y && lookDirection.x > 0.1f)
            animator.SetInteger("Facing", 1);
        if (AbsLookDirection.x > AbsLookDirection.y && lookDirection.x < -0.1f)
            animator.SetInteger("Facing", 3);
        if (AbsLookDirection.y > AbsLookDirection.x && lookDirection.y > 0.1f)
            animator.SetInteger("Facing", 0);
        if (AbsLookDirection.y > AbsLookDirection.x && lookDirection.y < -0.1f)
            animator.SetInteger("Facing", 2);
       
        // 캐릭터 공격               
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isAttack)
                StartCoroutine("Attack");
        }
    }

    public bool getIsAttack()
    {
        return isAttack;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        isAttack = true;
        yield return new WaitForSeconds(0.3f);
        isAttack = false;
    }
}
