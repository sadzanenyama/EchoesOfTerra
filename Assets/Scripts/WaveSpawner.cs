using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, BetweenWave};
    public SpawnState state = SpawnState.BetweenWave;
    [SerializeField] private bool onStartWave; 
    [SerializeField] protected WaveMasterSO waveData;

    [SerializeField] protected int waveNumber;

    protected float searchCountdown = 1f;
    [SerializeField] protected Transform spawnPointParent;

    protected Transform[] spawnPoints;

    protected virtual void Start()
    {
        spawnPoints = new Transform[spawnPointParent.childCount];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnPointParent.GetChild(i).transform;
        }
        if (onStartWave)
        {
            WaveStart();
        }
 
    }

    private void Update()
    {
        if (state == SpawnState.BetweenWave)
            return;

        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.BetweenWave;
        if (waveNumber >= waveData.waves.Length)
        {
            Debug.Log("Finished level");
            return;
        }
    }

    public void WaveStart()
    {
        waveNumber++;
        StartCoroutine(SpawnWave(waveData.waves[waveNumber]));
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown > 0) //Countdown so that we don't check for enemies every frame (Waste of resources)
            return true;

        int numEnemies = GameObject.FindGameObjectsWithTag("Melee").Length + GameObject.FindGameObjectsWithTag("Ranged").Length + GameObject.FindGameObjectsWithTag("Trooper").Length;

        if (numEnemies == 0)
        {
            Debug.Log("No enemy");
            searchCountdown = 1f;
            return false;
        }
        else
        {
            Debug.Log("Found " + numEnemies);
            searchCountdown = 1f;
            return true;
        }
    }

    IEnumerator SpawnWave(WaveSO wave)
    {
        state = SpawnState.Spawning;

        for (int i2 = 0; i2 < wave.groups.Length; i2++)
        {
            for (int i = 0; i < wave.groups[i2].numToSpawn; i++)
            {
                SpawnEnemy(wave.groups[i2].enemyToSpawn, wave, i2);
                yield return new WaitForSeconds(1f / wave.groups[i2].spawnRate);
            }

            yield return new WaitForSeconds(wave.groups[i2].delayToNextGroup);
        }

        state = SpawnState.Waiting;
    }

    void SpawnEnemy(GameObject _enemy, WaveSO wave, int groupNumber)
    {
        GameObject currentEnemy = ObjectPooler.Singleton.GetPooledObjectByTag(_enemy.tag);
        currentEnemy.transform.position = GetSpawnPosition(wave.groups[groupNumber].spawnPosition);
        currentEnemy.transform.rotation = Quaternion.identity;
        currentEnemy.SetActive(true);
    }

    public Vector3 GetSpawnPosition(WaveGroup.SpawnPositions spawn)
    {
        return spawnPoints[(int)spawn].position;
    }
}
