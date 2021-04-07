using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : BaseController
{
    private BuildMenu _buildMenu;
    private UpgradeMenu _upgradeMenu;
    private Button _startButton;

    TMP_Text _cashLabel;

    public UIController(GameSystem gameSystem) : base(gameSystem)
    {
        _cashLabel = gameSystem.CashLabel;
        _buildMenu = gameSystem.BuildMenu;
        _upgradeMenu = gameSystem.UpgradeMenu;
        _startButton = gameSystem.StartButton;

        _startButton.onClick.AddListener(() => 
        {
            gameSystem.GameController.StartGame();
            _startButton.gameObject.SetActive(false);
        });
    }

    public void Start()
    { 
        Refresh();
    }

    public void InitializeMenus(Dictionary<BuildingType, BuildingsData> data)
    {
        _buildMenu.Initialize(_gameSystem, data);
        _upgradeMenu.Initialize(_gameSystem);
    }

    public void ShowBuildMenu(BuildMenuData data)
    {
        _buildMenu.gameObject.SetActive(true);

        _buildMenu.Initilaize(data);
    }

    public void HideBuildMenu()
    {
        _buildMenu.gameObject.SetActive(false);
    }

    public void ShowUpgradeMenu(BuildMenuData data)
    {
        _upgradeMenu.gameObject.SetActive(true);

        _upgradeMenu.Initialize(data);
    }

    public void HideUpgradeMenu()
    {
        _upgradeMenu.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        RefreshCells();
        RefreshMenu();
    }

    public void RefreshCells()
    {
        _cashLabel.text = $"{_gameSystem.GameState.Cash} $";
    }

    public void RefreshMenu()
    {
        if (_buildMenu.gameObject.activeSelf)
            _buildMenu.Refresh();

        if (_upgradeMenu.gameObject.activeSelf)
            _upgradeMenu.Refresh();
    }
}
