using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MenuController : BaseController
{
    readonly string PATH = "";

    public MenuController(GameSystem gameSystem) : base(gameSystem)
    {

    }

    /*
    public T OpenMenu<T>()
    {
        string nameOfMenu = nameof(T);

        // Addressables.InstantiateAsync(PATH + nameOfMenu).Completed += () => { };

        return new T();
    }
    */
    
}
