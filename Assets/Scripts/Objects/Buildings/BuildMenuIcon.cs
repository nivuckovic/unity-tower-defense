using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuIcon : MonoBehaviour
{
    [SerializeField] Image _turretImage;
    [SerializeField] TMP_Text _turretLabel;
    [SerializeField] Image _overlay;
    [SerializeField] GameObject _buyOverlay;

    public void Initialize(GameSystem gameSystem, BuildingsData.BuildingLevel data)
    {
        _turretImage.sprite = data.Icon;
        _turretLabel.text = $"{data.Price} $";
    }

    public void SetOverlayActive(bool value)
    {
        _overlay.gameObject.SetActive(value);
    }
}
