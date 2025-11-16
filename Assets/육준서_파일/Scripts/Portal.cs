using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Portal : MonoBehaviour
{
    public float minSpawncool;
    public float spawncool;
    public List<Enemy> enemies_variable = new List<Enemy>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        spawncool = EnemyManager.Instance.maxSpawnCool;
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (EnemyManager.Instance.isSpawn)
            {
                float waitTime = Random.Range(minSpawncool, spawncool);
                yield return new WaitForSeconds(waitTime);
                int monstertype = Random.Range(0, enemies_variable.Count);
                GameObject enemy = GameObject.Instantiate(enemies_variable[monstertype].gameObject,this.transform.position,Quaternion.identity);
                enemy.transform.position = new Vector3(enemy.transform.position.x, 0,enemy.transform.position.z);
                EnemyManager.Instance.enemynum -= 1;
            }
        }
    }
}
