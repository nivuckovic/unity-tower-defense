using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSlot : MonoBehaviour
{
    private GameSystem _gameSystem;
    public bool Occupied { get; set; }
    private bool _selected; 
    

    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Transform _canvasParent;
    [SerializeField] private Button _slotPrefab;

    public Building Building;

    public void Initialize(GameSystem gameSystem)
    {
        _gameSystem = gameSystem;

        _canvasParent = GameObject.Find("UI/BuildingSlots").transform;

        Button slot = Instantiate(_slotPrefab, _canvasParent);
        slot.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        slot.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        _gameSystem.BuilderController.OnBuildingSlotClicked(this);
    }

    public void Select()
    {
        _selected = true;
        _sprite.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        _selected = false;
        _sprite.gameObject.SetActive(false);
    }
}
