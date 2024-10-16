using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, BetweenWave};
    public SpawnState state = SpawnState.BetweenWave;
    [SerializeField] private WaveMasterSO waveData;
    public int waveNumber;

    protected float searchCountdown = 1f;
    [SerializeField] private Transform spawnPointParent;

    protected Transform[] spawnPoints;

    public static WaveSpawner instance;

    public List<int> wavesToShowDialogue = new List<int>();
    bool shownDialogue;

    bool finishedLevel = false;

    private AudioSource audioSource;
    public AudioClip victorySound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;

        state = SpawnState.BetweenWave;

        spawnPoints = new Transform[spawnPointParent.childCount];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnPointParent.GetChild(i).transform;
        }

        Dialogue.instance.DisplayMainMessage();
        shownDialogue = true;
    }

    private void Update()
    {
        if (finishedLevel)
            return;

        if (state == SpawnState.BetweenWave)
        {
            if (wavesToShowDialogue.Contains(waveNumber))
            {
                if(!shownDialogue)
                {
                    StartCoroutine(ShowDialogue());
                    shownDialogue = true;
                }
            }
            else
            {
                WaveStart();
            }
            return;
        }

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

    IEnumerator ShowDialogue()
    {
        yield return new WaitForSeconds(2f);
        Dialogue.instance.DisplayMainMessage();
    }

    void WaveCompleted()
    {
        waveNumber++;
        state = SpawnState.BetweenWave;
        if (waveNumber >= waveData.waves.Length)
        {
            StartCoroutine(FinishLevel());
        }
    }

    IEnumerator FinishLevel()
    {
        finishedLevel = true;
        PauseManager.Pause(false);
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);

        audioSource.PlayOneShot(victorySound, 0.5f);

        yield return new WaitForSeconds(2.5f);

        if(SceneManager.GetActiveScene().name == "SampleScene")
            SceneManager.LoadScene("Main");
        else if(SceneManager.GetActiveScene().name == "NewTerra")
            SceneManager.LoadScene("SaveNewTerra");
        else if (SceneManager.GetActiveScene().name == "OldEarth")
            SceneManager.LoadScene("SaveOldEarth");

    }

    public void WaveStart()
    {
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

        shownDialogue = false;

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

    public int GetNumWaves()
    {
        return waveData.waves.Length;
    }
}
