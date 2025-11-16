using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using JetBrains.Annotations;

public enum EnemyType
{
    range,
    speed,
    defence,
    Boss
}

public enum Act
{
    move,
    attack
}
public class Enemy : MonoBehaviour
{
    Animator animator;
    LineRenderer line;
    Rigidbody rigid;
    public bool isMove = true;
    public bool isDie = false;
    public GameObject target;
    public GameObject target_attack;
    public GameObject attackobj;
    public EnemyType enemyType;
    public Act act;
    public Enemy_status statusData;
    Enemy_status status;
    public float basicSpeed;
    public float curSpeed;
    public float maxHp;
    public float curHp;
    public float basicAttackCool;
    public float curAttackCool;
    public float attackTimer;
    public float attackDelay;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        status = ScriptableObject.Instantiate(statusData);
        rigid = GetComponent<Rigidbody>();
        basicSpeed = status.speed;
        curSpeed = basicSpeed;
        maxHp = status.hp;
        curHp = maxHp;
        basicAttackCool = status.attackCoolTime;
        curAttackCool = status.attackCoolTime;
        attackDelay = status.attackDelay;
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       
        target = GameObject.FindGameObjectWithTag("castle");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDie)
        {
            if(act == Act.move)
            {
                attackTimer += Time.deltaTime;
            }
            if (isMove)
            {
                line.enabled = false;
                line.startWidth=(0.25f);
                line.endWidth = 0.25f;
                transform.LookAt(target.transform.position);
                transform.position += transform.forward * curSpeed * Time.deltaTime;
                animator.SetBool("move", true);
            }
            if(attackTimer >= curAttackCool && enemyType == EnemyType.range && GameObject.FindWithTag("Player") != null)
            {
                isMove = false;
                act = Act.attack;
                StartCoroutine(Attack());
                attackTimer = 0;
            }

            
        }
    }


    IEnumerator Attack()
    {
        
        isMove=false;
        animator.SetBool("move", false);
        line.enabled = true;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        int ran = Random.Range(0, targets.Length);
        Vector3 attackPos;
        target_attack = targets[ran];

        line.SetPosition(0,this.transform.position);
        line.SetPosition(1,target_attack.transform.position);
        transform.LookAt(target_attack.transform.position);
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackDelay);
        attackPos =line.GetPosition(1);
        line.SetPosition(1,attackPos);
        GameObject AttackObj = GameObject.Instantiate(attackobj,transform.position,Quaternion.identity);
        AttackObj.GetComponent<EnemyAttackObj>().target = target_attack;
        
        act = Act.move;
        
        line.enabled = false;
        isMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("castle"))
        {
            Destroy(this.gameObject);
        }
    }


    
    public void SetHp(float damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            curHp = 0;
            isDie = true;
            Destroy(this.gameObject);
        }
    }
}
