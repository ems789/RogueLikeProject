﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterType
    {
        MELEE,
        RANGER,
        DASH,
        NumberOfTypes,
    }

    // 타입 관련
    public MonsterType monsterType;

    public bool isBoss = false;
    public bool openStage = false;

    public GameObject pattern;
    private Animator animator;
    private Transform targetTrans;
    private Vector3 tempDir;
    private Vector3 tempPos;
    private Vector3 targetPos;

    private Vector3 lookDirection = new Vector3(0, 0, 0);

    private float scope;
    private float distance;

    // 스테이터스
    public int hp;
    private int currentHP;
    public int attackDamage;
    private float attackRange;    
    public float moveSpeed;
    

    public float delayInAdvance; // 선딜
    public float delayLator; // 후딜

    private bool isMove = false;
    private bool isPatrol = false;
    public bool isAttack = false; // 현재 공격중인지
    public bool isPattern = false; // 패턴 발동중에는 공격 중지

    private float projectileLiveTime;
    private float shotSpeed;

    private float checkTime;
    private float attackCoolTime;
    private float patrolCoolTime;
    private float movingTime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        targetTrans = GameObject.FindWithTag("Player").transform;
        targetPos = targetTrans.position;
        distance = Vector3.Distance(targetPos, gameObject.transform.position);

        currentHP = hp;
        scope = 4.5f;
        patrolCoolTime = 5f;

        // 플레이어 근처에 생성되면 제거
        if (PlayerInScope())
        {
            gameObject.SetActive(false);
            MonsterManager.instance.monsterCnt--;
        }

        // 나중에 몬스터별로 초기화로 변경
        if (monsterType == MonsterType.MELEE)
        {
            projectileLiveTime = 0.8f;
            shotSpeed = 100f; // *임시* 몬스터 별로 다를 수 있음
            attackRange = 1.4f;
            attackCoolTime = 0.8f;
        }
        else if(monsterType == MonsterType.RANGER)
        {
            projectileLiveTime = 2f;
            shotSpeed = 150f;
            attackRange = 3.5f;
            attackCoolTime = 1.0f;
        }
        else if (monsterType == MonsterType.DASH)
        {
            shotSpeed = 3f; // 대쉬 타입은 몬스터가 투사체
            attackRange = 2.5f;
            attackCoolTime = 1.2f;
        }

        if (isBoss)
        {
            attackRange += 1; // *임시* 스케일 증가에 따른 공격 범위 증가
            shotSpeed = 200f;
        }
    }

    private void Update()
    {

        targetPos = targetTrans.position;
        distance = Vector3.Distance(targetPos, gameObject.transform.position);

        if(PlayerInScope())
        {
            animator.SetBool("Walk", true);

            // 공격하고 있을때는 이동 불가
            if(!isAttack) 
                Move();

            if (PlayerInAttackRange())
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                animator.SetBool("Walk", false);

                checkTime += Time.deltaTime;
                if (checkTime >= attackCoolTime)
                {
                    if (monsterType == MonsterType.DASH && !isAttack)
                    {
                        DashPath();
                        isAttack = true;
                        StartCoroutine("Dash");
                        checkTime = 0;
                    }
                    if (monsterType != MonsterType.DASH)
                    {
                        if (!isPattern)
                        {
                            Attack();
                            checkTime = 0;
                        }
                    }
                }                
                return;
            }
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            animator.SetBool("Walk", false);

            if (isPatrol)
                Patrol();

            checkTime += Time.deltaTime;
            if(checkTime >= patrolCoolTime)
            {                
                checkTime = 0;
                isPatrol = true;
                patrolCoolTime = Random.Range(4.0f, 6.0f);
                lookDirection.x = Random.Range(-1, 2);
                lookDirection.y = Random.Range(-1, 2);                
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {        
        checkTime += Time.deltaTime;
        while (checkTime >= 1.5) // 1초당 데미지
        {
            checkTime = 0;
            if (collision.transform.tag == "Player")
            {
                Player.instance.TakeDamage((attackDamage));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Player.instance.TakeDamage(attackDamage);
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine("WaitCoroutine");
    }

    void Patrol()
    {
        animator.SetBool("Walk", true);

        transform.position += lookDirection * moveSpeed * Time.deltaTime;

        movingTime = Random.Range(2.0f, 3.0f);
        checkTime += Time.deltaTime;
        if (checkTime >= movingTime)
        {
            checkTime = 0;
            isPatrol = false;
        }
    }


    void Move()
    {
        if (animator.GetBool("Walk"))
        {
            if (targetPos.x < transform.position.x)
                lookDirection.x = -1;
            else if (targetPos.x > transform.position.x)
                lookDirection.x = 1;
            if (targetPos.y < transform.position.y)
                lookDirection.y = -1;
            else if (targetPos.y > transform.position.y)
                lookDirection.y = 1;

            if (!PlayerInAttackRange())
                transform.position += lookDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            MonsterManager.instance.monsterCnt--;

            if (MonsterManager.instance.monsterCnt == 5) // 출구가 열리는 조건
            {
                GameManager.instance.StartCoroutine("ExitOpenText");
                BoxCollider2D exitCol = GameObject.FindWithTag("Exit").GetComponent<BoxCollider2D>();
                exitCol.enabled = true;
            }
        }
        Debug.Log(currentHP);
    }

    // 딜레이, 데미지 처리
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(delayInAdvance);
        isMove = false;
        if (PlayerInAttackRange())
        {
            Shot();            
        }
        yield return new WaitForSeconds(delayLator);
        isMove = true;
        animator.ResetTrigger("Attack");
        StopAllCoroutines();
    }

    public bool PlayerInScope()
    {
        if (distance <= scope)
            return true;
        else
            return false;
    }

    public bool PlayerInAttackRange()
    {
        if (distance <= attackRange)
            return true;
        else
            return false;
    }

    private void DashPath()
    {
        tempPos = targetPos; // 타겟 위치 설정
        tempDir = targetPos - transform.position;

        GameObject myPattern = Instantiate(pattern
            , new Vector3(transform.position.x, transform.position.y, transform.position.z)
            , Quaternion.identity);

        FaceObject(myPattern);
        myPattern.GetComponentInChildren<Animator>().Play("Normal");

        Destroy(myPattern, 1.3f);
        
    }

    IEnumerator Dash()
    {
        // 몬스터와 플레이어의 거리 차이가 0.45이하로 붙으면
        while(Mathf.Abs((Mathf.Abs(transform.position.x) - Mathf.Abs(tempPos.x))) > 0.25 ||
                 (Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(tempPos.y))) > 0.25)
        {
            yield return new WaitForSeconds(0.001f);
            transform.Translate(tempDir.normalized * shotSpeed * Time.deltaTime);
        }

        yield return new WaitForSeconds(attackCoolTime);
        isAttack = false;                   
    }

    private void Shot()
    {
        // 리스트에 있는 발사 예정중인 다음 발사체 얻어오기
        GameObject projectle = ProjectlePool.ProjectilePool[(int)monsterType].PeekObject();
        Rigidbody2D ProjectileRigid = projectle.GetComponent<Rigidbody2D>();

        projectle.GetComponent<Projectile>().SetProject(attackDamage, attackRange, projectileLiveTime);
        ProjectlePool.ProjectilePool[(int)monsterType].GetObject(transform.position.x, transform.position.y);
        FaceObject(projectle);
        
        // 타겟 위치로 발사
        Vector2 ToPlayerDir = targetPos - new Vector3(transform.position.x, transform.position.y);        
        ProjectileRigid.AddForce(ToPlayerDir.normalized * shotSpeed);              
    }

    // 오브젝트가 플레이어를 바라보도록 회전
    private void FaceObject(GameObject obj)
    {
        float digree;

        digree = Mathf.Atan2(targetPos.y - transform.position.y
            , targetPos.x - transform.position.x) * 180f / Mathf.PI;

        obj.transform.Rotate(0, 0, digree);        
    }
}

