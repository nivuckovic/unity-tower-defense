using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private GameSystem _gameSystem;
    public int Cash;
    public int Lives;

    public GameState(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }

    public void Give(int amount)
    {
        Cash += amount;

        _gameSystem.UIController.Refresh();
    }

    public void Take(int amount)
    {
        Cash -= amount;

        _gameSystem.UIController.Refresh();
    }

    public bool CanTake(int amount)
    {
        return Cash - amount >= 0;
    }
}
