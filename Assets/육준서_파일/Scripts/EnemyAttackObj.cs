using UnityEngine;

public class EnemyAttackObj : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public Vector3 ran;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("adf");
        this.transform.LookAt(target.transform.position);
        ran = target.transform.position - this.transform.position ;
    }

    private void Awake()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
        transform.position +=ran * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
