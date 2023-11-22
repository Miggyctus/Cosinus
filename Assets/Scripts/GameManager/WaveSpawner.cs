using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING, WAITING, COUNTING
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int count;
        public float rate;
    }

    public Wave waves;
    public float timeBetweenWaves = 5f;
    public float waveCountDown = 0f;
    public float xPos;
    public float zPos;

    //private int nextWave = 0;
    public SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
        waves.count = 3;
        waves.rate = 1f;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            //check if enemies are still alive
            if(!EnemyIsAlive())
            {
                //begin new wave
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }
        if(waveCountDown <= 0)
        {
            if(state != SpawnState.SPAWNING )
            {
                StartCoroutine(SpawnWave(waves));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    void WaveCompleted()
    {
        Debug.Log("wave completed");
        
        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        waves.count += Random.Range(2, 5);
        waves.rate += 0.2f;
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave...");
        state = SpawnState.SPAWNING;

        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        
        state = SpawnState.WAITING;
        
        yield break;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Debug.Log("Spawning Churro ");
        xPos = Random.Range(-16.5f, 9.4f);
        zPos = Random.Range(6.6f, 29.7f);
        Instantiate(enemy, new Vector3(xPos, 0.1f, zPos), transform.rotation);
    }

}
