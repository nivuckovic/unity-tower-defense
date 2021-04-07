using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using PathCreation;

public class MapController : BaseController
{
    AsyncOperationHandle levelHandle;
    Level _currentLevel;
    public Level CurrentLevel => _currentLevel;

    private int _currentWave = 0;

    public MapController(GameSystem gameSystem) : base(gameSystem)
    {

    }

    public void Start()
    {
        LoadLevel("Levels.Level1");
    }

    public void Update()
    {
    }

    #region EnemySpawning
    public IEnumerator StartSpawningEnemies()
    {
        foreach(LevelData.WaveData waveData in CurrentLevel.LevelData.LevelWavesData)
        {
            foreach (LevelData.WaveEnemy waveEnemy in waveData.ListOfEnemies)
            {
                _gameSystem.Scheduler.StartCoroutine(SpawnEnemy(waveEnemy));
            }

            yield return new WaitForSeconds(waveData.NextWaveTime);
        }
    }

    private IEnumerator SpawnEnemy(LevelData.WaveEnemy waveEnemy)
    {
        for (int i = 0; i < waveEnemy.Amount; ++i)
        {
            Enemy enemy = GameObject.Instantiate(CurrentLevel.EnemyPrefab, _gameSystem.MapController.CurrentLevel.Enemies);
            enemy.Initialize(_gameSystem, GetEnemyData(waveEnemy.Type), GetRandomPath());
            enemy.name = (GetEnemyData(waveEnemy.Type).EnemyType == EnemyType.Tank) ? "Tank" : "Soldier";
            _gameSystem.EnemyController.ListOfEnemies.Add(enemy);

            yield return new WaitForSeconds(waveEnemy.DeltaTime);
        }

    }
    #endregion
    #region LoadLevel
    public void LoadLevel(string address)
    {
        if (!levelHandle.IsDone)
            return;

        if (CurrentLevel != null)
            Addressables.Release(levelHandle);

        levelHandle = Addressables.InstantiateAsync(address, parent: _gameSystem.transform);
        levelHandle.Completed += OnLevelHandleCompleted;
    }

    private void OnLevelHandleCompleted(AsyncOperationHandle handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            _currentLevel = (handle.Result as GameObject).GetComponent<Level>();
            _currentLevel.name = _currentLevel.name.Replace("(Clone)", "");
            _currentLevel.Initialize(_gameSystem);

            _gameSystem.GameController.OnLevelLoaded();
        }
    }
    #endregion

    private EnemyData GetEnemyData(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Soldier:
                return _currentLevel.LevelData.Soldier;
            case EnemyType.Tank:
                return _currentLevel.LevelData.Tank;
        }

        return _currentLevel.LevelData.Soldier;
    }

    private PathCreator GetRandomPath()
    {
        System.Random rand = new System.Random();

        return CurrentLevel.Paths[rand.Next(CurrentLevel.Paths.Length)];
    }
}
