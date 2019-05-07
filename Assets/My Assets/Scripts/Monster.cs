using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonsterType
    {
        MELEE,
        RANGER,
        //DASH,
        NumberOfTypes,
    }
    public MonsterType monsterType;

    public GameObject projectile;
    private Rigidbody2D projectileRigid;

    private Animator animator;
    private Transform targetTrans;
    private Vector3 targetVec;

    private Vector3 lookDirection = new Vector3(0, 0, 0);

    private float scope;
    private float distance;

    public int hp;
    private int currentHP;
    public int attackDamage;
    private float attackRange;    
    public float moveSpeed;

    public float delayInAdvance; // 선딜
    public float delayLator; // 후딜

    private bool isMove = false;
    private bool isPatrol = false;

    private float projectileLiveTime;
    private float shotSpeed;

    private float checkTime;
    private float attackCoolTime;
    private float patrolCoolTime;
    private float movingTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        targetTrans = GameObject.FindWithTag("Player").transform;
        targetVec = targetTrans.position;
        distance = Vector3.Distance(targetVec, gameObject.transform.position);

        currentHP = hp;
        scope = 4.5f;
        patrolCoolTime = 5f;

        // 나중에 몬스터별로 초기화로 변경
        if (monsterType == MonsterType.MELEE)
        {
            projectileLiveTime = 0.8f;
            shotSpeed = 100f; // 임시 몬스터 별로 다를 수 있음
            attackRange = 1.4f;
            attackCoolTime = 0.8f;
        }
        else if(monsterType == MonsterType.RANGER)
        {
            projectileLiveTime = 2f;
            shotSpeed = 150f;
            attackRange = 3f;
            attackCoolTime = 1.0f;

        }
    }

    private void Update()
    {
        targetVec = targetTrans.position;
        distance = Vector3.Distance(targetVec, gameObject.transform.position);

        if(PlayerInScope())
        {
            animator.SetBool("Walk", true);
            Move();

            if (PlayerInAttackRange())
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                animator.SetBool("Walk", false);

                checkTime += Time.deltaTime;
                if (checkTime >= attackCoolTime)
                {
                    Attack();
                    checkTime = 0;
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

    private void Attack()
    {
        animator.SetTrigger("Attack");
        StartCoroutine("WaitCoroutine");
    }

    void Patrol()
    {
        animator.SetBool("Walk", true);

        transform.position += lookDirection * moveSpeed * Time.deltaTime;

        //test
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
            if (targetVec.x < transform.position.x)
                lookDirection.x = -1;
            else if (targetVec.x > transform.position.x)
                lookDirection.x = 1;
            if (targetVec.y < transform.position.y)
                lookDirection.y = -1;
            else if (targetVec.y > transform.position.y)
                lookDirection.y = 1;

            if (!PlayerInAttackRange())
                transform.position += lookDirection * moveSpeed * Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            Destroy(gameObject);
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


    private void Shot()
    {
        GameObject projectileCopy = Instantiate(projectile);
        projectileCopy.transform.SetParent(gameObject.transform);
        projectileCopy.transform.position = gameObject.transform.position;       

        FaceObject(projectileCopy);        

        projectileRigid = projectileCopy.GetComponent<Rigidbody2D>();
        Vector2 ToPlayerDir = targetVec - new Vector3(transform.position.x, transform.position.y);        
        projectileRigid.AddForce(ToPlayerDir.normalized * shotSpeed);        

        Destroy(projectileCopy, projectileLiveTime);        
    }

    // 오브젝트가 대상을 바라보도록 회전
    private void FaceObject(GameObject obj)
    {
        float digree;

        digree = Mathf.Atan2(targetVec.y - transform.position.y
            , targetVec.x - transform.position.x) * 180f / Mathf.PI;

        obj.transform.Rotate(0, 0, digree);        
    }
}

