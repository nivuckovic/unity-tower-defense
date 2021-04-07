using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct BuildMenuData
{
    public Vector3 position;
    public BuildingsData BuildingData;
    public int BuildingLevel;
    public int MaxBuildingLevel;
}

public enum BuildingType
{
    Turret,
    RocketTurret,
    Third,
    Forth
}

public class BuildMenu : MonoBehaviour
{
    GameSystem _gameSystem;

    [SerializeField] BuildMenuIcon _turretIconBI;
    [SerializeField] BuildMenuIcon _rocketTurretBI;
    [SerializeField] BuildMenuIcon _thirdTurretBI;
    [SerializeField] BuildMenuIcon _forthTurretBI;

    private Dictionary<BuildingType, BuildMenuIcon> _buildMenuIcons = new Dictionary<BuildingType, BuildMenuIcon>();
    const int FirstLevel = 0;
    Dictionary<BuildingType, BuildingsData> _data;

    public void Initialize(GameSystem gameSystem, in Dictionary<BuildingType, BuildingsData> data)
    {
        _gameSystem = gameSystem;
        _data = data;

        _buildMenuIcons[BuildingType.Turret] = _turretIconBI;
        _buildMenuIcons[BuildingType.RocketTurret] = _rocketTurretBI;
        _buildMenuIcons[BuildingType.Third] = _thirdTurretBI;
        _buildMenuIcons[BuildingType.Forth] = _forthTurretBI;

        foreach(BuildingType type in Enum.GetValues(typeof(BuildingType)))
        {
            _buildMenuIcons[type].Initialize(gameSystem, data[type].Data[FirstLevel]);
        }

        Refresh();
    }

    public void Initilaize(BuildMenuData data)
    {
        transform.position = data.position;

        Refresh();
    }

    public void Refresh()
    {
        foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
        {
            _buildMenuIcons[type].SetOverlayActive(!_gameSystem.GameState.CanTake(_data[type].Data[FirstLevel].Price));
        }
    }

    public void OnFirstBuyButtonClicked() => _gameSystem.BuilderController.BuildOnSlot(BuildingType.Turret);
    public void OnSecondBuyButtonClicked() => _gameSystem.BuilderController.BuildOnSlot(BuildingType.RocketTurret);
    public void OnThirdBuyButtonClicked() => _gameSystem.BuilderController.BuildOnSlot(BuildingType.Third);
    public void OnForthBuyButtonClicked() => _gameSystem.BuilderController.BuildOnSlot(BuildingType.Forth);
}
