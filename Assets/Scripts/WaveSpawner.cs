using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, BetweenWave};
    public SpawnState state = SpawnState.BetweenWave;

    public WaveMasterSO waveData;

    [SerializeField] private int waveNumber;
    [SerializeField] private int groupNumber;

    private float searchCountdown = 1f;

    public Transform[] spawnPoints;

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
        //Insert event here
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

        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

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
                SpawnEnemy(wave.groups[i2].enemyToSpawn, wave);
                yield return new WaitForSeconds(1f / wave.groups[i2].spawnRate);
            }

            yield return new WaitForSeconds(wave.groups[i2].delayToNextGroup);
        }

        state = SpawnState.Waiting;
    }

    void SpawnEnemy(GameObject _enemy, WaveSO wave)
    {
        GameObject currentEnemy = ObjectPooler.Singleton.GetPooledObjectByTag(_enemy.tag);
        currentEnemy.transform.position = GetSpawnPosition(wave.groups[groupNumber].spawnPosition);
        currentEnemy.transform.rotation = Quaternion.identity;
        currentEnemy.SetActive(true);
    }

    public Vector3 GetSpawnPosition(WaveGroup.SpawnPositions spawn)
    {
        switch (spawn)
        {
            case WaveGroup.SpawnPositions.North:
                return spawnPoints[0].position;
            case WaveGroup.SpawnPositions.NorthEast:
                return spawnPoints[1].position;
            case WaveGroup.SpawnPositions.East:
                return spawnPoints[2].position;
            case WaveGroup.SpawnPositions.SouthEast:
                return spawnPoints[3].position;
            case WaveGroup.SpawnPositions.South:
                return spawnPoints[4].position;
            case WaveGroup.SpawnPositions.SouthWest:
                return spawnPoints[5].position;
            case WaveGroup.SpawnPositions.West:
                return spawnPoints[6].position;
            case WaveGroup.SpawnPositions.NorthWest:
                return spawnPoints[7].position;
            default:
                return Vector3.zero; // Fallback in case no direction is matched
        }
    }
}
