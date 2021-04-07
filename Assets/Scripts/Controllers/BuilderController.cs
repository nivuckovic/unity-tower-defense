using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BuilderController : BaseController
{
    public Transform _buildingSlotsContainer;
    private List<BuildingSlot> _buildingSlots = new List<BuildingSlot>();

    BuildingSlot _selectedBuildingSlot;
    private Building SelectedBuilding => _selectedBuildingSlot.Building;
    public int SelectedBuildingPrice => SelectedBuilding.Price;
    public int SelectedBuildingUpgradePrice => SelectedBuilding.UpgradePrice;

    bool _isBuildingSlotSelected;

    Dictionary<BuildingType, BuildingsData> _buildingsData;

    private IEnumerator _loadBuildingDataHandle;
    const int FirstLevel = 0;

    public BuilderController(GameSystem gameSystem) : base(gameSystem)
    {
        LoadBuildingData();
    }

    private void LoadBuildingData()
    {
        _loadBuildingDataHandle = LoadBuildingDataAsync();
        _loadBuildingDataHandle.MoveNext();
    }

    private IEnumerator LoadBuildingDataAsync()
    {
        _buildingsData = new Dictionary<BuildingType, BuildingsData>();

        foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
        {
            Addressables.LoadAssetAsync<BuildingsData>($"BuildingData.{type:g}").Completed +=
                (AsyncOperationHandle<BuildingsData> handle) =>
                {
                    _buildingsData[type] = handle.Result;
                    _loadBuildingDataHandle.MoveNext();
                };
        }

        foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
        {
            yield return null;
        }

        InitializeBuildMenu();
    }

    private void InitializeBuildMenu()
    {
        _gameSystem.UIController.InitializeMenus(_buildingsData);
    }

    public void OnLevelLoaded()
    {
        InitializeBuildingSlotes();
    }

    private void InitializeBuildingSlotes()
    {
        _buildingSlotsContainer = _gameSystem.MapController.CurrentLevel.BuildingSlots;
        foreach (Transform go in _buildingSlotsContainer)
        {
            BuildingSlot buildingSlot = go.GetComponent<BuildingSlot>();
            buildingSlot.Initialize(_gameSystem);

            _buildingSlots.Add(buildingSlot);
        }
    }

    public void OnBuildingSlotClicked(BuildingSlot buildingSlot)
    {
        // Vec je selektiran neki slot
        if(_isBuildingSlotSelected)
        {
            bool clickedOnSelectedSlot = buildingSlot != _selectedBuildingSlot;
            Deselect();

            // Kliknuo na slot koji nije bio selektiran
            if (clickedOnSelectedSlot)
            {
                Select(buildingSlot);
            }
        }
        // Nijedan slot nije sleketiran
        else
        {
            Select(buildingSlot);
        }
    }

    private void Select(BuildingSlot buildingSlot)
    {
        buildingSlot.Select();
        _isBuildingSlotSelected = true;
        _selectedBuildingSlot = buildingSlot;

        if (buildingSlot.Occupied)
            _gameSystem.UIController.ShowUpgradeMenu(GetMenuData(_selectedBuildingSlot));
        else
            _gameSystem.UIController.ShowBuildMenu(GetMenuData(_selectedBuildingSlot));
        
    }

    private void Deselect()
    {
        _selectedBuildingSlot.Deselect();
        _gameSystem.UIController.HideBuildMenu();
        _gameSystem.UIController.HideUpgradeMenu();

        _isBuildingSlotSelected = false;
        _selectedBuildingSlot = null;
    }

    public void BuildOnSlot(BuildingType type)
    {
        Building building = GameObject.Instantiate(_gameSystem.MapController.CurrentLevel.BuildingPrefab, _buildingSlotsContainer);
        building.Initialize(_gameSystem, _buildingsData[type]);
        building.transform.position = _selectedBuildingSlot.transform.position;

        _selectedBuildingSlot.Occupied = true;
        _selectedBuildingSlot.Building = building;

        _gameSystem.GameState.Take(SelectedBuilding.Price);

        Deselect();
    }

    public void UpgradeBuilding()
    {
        Building building = _selectedBuildingSlot.Building;
        building.Upgrade();

        _gameSystem.GameState.Take(SelectedBuilding.Price);
        Deselect();
    }

    public void SellBuilding()
    {
        Destroy(_selectedBuildingSlot.Building.gameObject);
        _selectedBuildingSlot.Occupied = false;

        _gameSystem.GameState.Give((int)Mathf.Floor(SelectedBuilding.Price * 0.75f)); 
        Deselect();
    }

    private BuildMenuData GetMenuData(BuildingSlot slot)
    {
        BuildMenuData menuData = new BuildMenuData();
        
        menuData.position = Camera.main.WorldToScreenPoint(slot.transform.position);
        if (slot.Occupied)
        {
            menuData.BuildingData = _buildingsData[slot.Building.Type];
            menuData.BuildingLevel = slot.Building.Level;
            menuData.MaxBuildingLevel = _buildingsData[slot.Building.Type].MaxLevel;
        }

        return menuData;
    }
}
