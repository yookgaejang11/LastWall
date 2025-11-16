using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;

    public int enemynum;
    public int maxEnemynum;
    public bool isSpawn = true;
    public List<GameObject> portals = new List<GameObject>();
    public float maxSpawnCool;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemynum = maxEnemynum;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemynum <= 0)
        {
            isSpawn = false;
        }
        else
        {
            isSpawn = true;
        }
    }

    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
}
