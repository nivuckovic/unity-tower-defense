using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected GameSystem _gameSystem;

    public BaseController(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }
}
