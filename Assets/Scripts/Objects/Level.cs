using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Level : MonoBehaviour
{
    public PathCreator[] Paths;
    public LevelData LevelData;
    public Transform Spawner;
    public Transform Enemies;
    public Enemy EnemyPrefab;
    public Building BuildingPrefab;
    public Transform BuildingSlots;

    public List<EnemyType> EnemiesLeftInWave;

    private GameSystem _gameSystem;
    public void Initialize(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;

        CalculateEnemiesInWave(0);
    }

    public void CalculateEnemiesInWave(int currentWave)
    {
        foreach(LevelData.WaveEnemy waveEnemy in LevelData.LevelWavesData[currentWave].ListOfEnemies)
        {
            for(int i = 0; i < waveEnemy.Amount; ++i)
            {
                EnemiesLeftInWave.Add(waveEnemy.Type);
            }
        }
    }
}
