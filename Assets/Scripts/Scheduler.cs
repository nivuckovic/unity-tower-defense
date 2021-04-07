using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{
    private GameSystem _gameSystem;
    private List<IEnumerator> StartedCoroutines = new List<IEnumerator>();
    private IEnumerator test; 
    public void SetGameSystem(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }

    public void InvokeIn(Action func, float time)
    {
        StartCoroutine(InvokeInCoroutine(func, time));
    }

    private IEnumerator InvokeInCoroutine(Action func, float time) 
    {
        yield return new WaitForSeconds(time);

        func.Invoke();
    }
}
