using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public GameObject enemy;
    public float xPos;
    public float zPos;
    public int enemyCount = 0;
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(enemyCount < 10)
        {
            xPos = Random.Range(-16.5f, 9.4f);
            zPos = Random.Range(6.6f, 29.7f);
            Instantiate(enemy, new Vector3(xPos, 0.1f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1);
            enemyCount++;
        }
    }
    
}
