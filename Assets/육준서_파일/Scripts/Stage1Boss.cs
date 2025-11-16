using System.Collections;
using UnityEngine;

public class Stage1Boss : MonoBehaviour
{
    Animator animator;
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
        if (!isDie)
        {
            if (act == Act.move)
            {
                attackTimer += Time.deltaTime;
            }
            if (isMove)
            {
                transform.LookAt(target.transform.position);
                transform.position += transform.forward * curSpeed * Time.deltaTime;
                animator.SetBool("move", true);
            }
            if (attackTimer >= curAttackCool && GameObject.FindWithTag("Player") != null)
            {
                isMove = false;
                act = Act.attack;
                Pattern();
                
            }


        }
    }

    void Pattern()
    {
        
        int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            StartCoroutine(Pattern1());
        }
        else
        {
            StartCoroutine (Pattern2());
        }
        attackTimer = 0;
    }


    IEnumerator Pattern1()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            yield return null;
        }
        isMove = false;
        animator.SetBool("move", false);
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        int ran = Random.Range(0, targets.Length);
        target_attack = targets[ran];

        transform.LookAt(target_attack.transform.position);
        animator.SetTrigger("skill1");
        yield return new WaitForSeconds(1.5f);
        GameObject AttackObj = GameObject.Instantiate(attackobj, transform.position, Quaternion.identity);
        AttackObj.GetComponent<EnemyAttackObj>().target = target_attack;
        yield return new WaitForSeconds(2.5f);
        act = Act.move;

        isMove = true;
    }


    IEnumerator Pattern2()
    {
        isMove = false;
        animator.SetBool("move", false);

        animator.SetTrigger("skill2");
        EnemyManager.Instance.enemynum += 4;
        yield return new WaitForSeconds(2);
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
        if (curHp <= 0)
        {
            StartCoroutine(Die());   
            
        }
    }

    IEnumerator Die()
    {
        curHp = 0;
        isDie = true;
        animator.SetBool("die", true);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
