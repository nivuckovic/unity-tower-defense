using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : BaseController
{

    public GameController(GameSystem gameSystem) : base(gameSystem)
    {
    }

    public void Start()
    {
        _gameSystem.GameState.Cash = 125;
        _gameSystem.GameState.Lives = 1;

        _gameSystem.MapController.Start();
        _gameSystem.UIController.Start();
    }

    public void OnLevelLoaded()
    {
        _gameSystem.BuilderController.OnLevelLoaded();
    }

    public void StartGame()
    {
        _gameSystem.Scheduler.StartCoroutine(_gameSystem.MapController.StartSpawningEnemies());
    }

    public void Update()
    {
        _gameSystem.MapController.Update();
    }
}
