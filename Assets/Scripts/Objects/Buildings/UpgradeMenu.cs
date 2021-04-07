using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    GameSystem _gameSystem;

    [SerializeField] BuildMenuIcon _upgradeIcon;
    [SerializeField] BuildMenuIcon _sellIcon;
    [SerializeField] TMP_Text _sellLabel;

    public void Initialize(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;
    }

    public void Initialize(BuildMenuData data)
    {
        if(data.BuildingLevel + 1 < data.MaxBuildingLevel)
        {
            _upgradeIcon.Initialize(_gameSystem, data.BuildingData.Data[data.BuildingLevel + 1]); // TODO: dohvati iz gamesystema
            _sellLabel.text = $"{Mathf.FloorToInt(_gameSystem.BuilderController.SelectedBuildingPrice * 0.75f)} $";
        }
        else
        {
            _upgradeIcon.gameObject.SetActive(false);
        }


        transform.position = data.position;

        Refresh();
    }

    public void Refresh()
    {
        _upgradeIcon.SetOverlayActive(!_gameSystem.GameState.CanTake(_gameSystem.BuilderController.SelectedBuildingUpgradePrice));
    }

    public void OnUpgradeButtonClicked() => _gameSystem.BuilderController.UpgradeBuilding();
    public void OnSellButtonClicked() => _gameSystem.BuilderController.SellBuilding();
}
 