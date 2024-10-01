using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave0X", menuName = "SO/Wave")]
public class WaveSO : ScriptableObject
{
    public WaveGroup[] groups;
}

[System.Serializable]
public class WaveGroup
{
    public GameObject enemyToSpawn;
    public float spawnRate;
    public float numToSpawn;
    public float delayToNextGroup;
    public enum SpawnPositions { North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest };
    public SpawnPositions spawnPosition;
}
