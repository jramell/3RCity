using System;
using UnityEngine;
using UnityEngine.UI;


public class BuildButton : MonoBehaviour, IMoneyChangedListener {

    [SerializeField]
    private Buildings.Type building;

    Buildable buildingAttributes;
    Button buttonComponent;
    
    void Awake()
    {
        buttonComponent = GetComponent<Button>();
        GameObject buildingPrefab = Managers.PrefabManager.MapBuildingToPrefab(building);
        buildingAttributes = buildingPrefab.GetComponent<Buildable>();
        Activate();
    }

    public void OnClick()
    {
        Managers.BuildingPlacementManager.PreviewBuilding(building);
    }

    private bool Interactable {
        set {
            buttonComponent.interactable = value;
        }
    }

    public void Deactivate() {
        CityController.Current.RemoveMoneyChangedListener(this);
        if (buttonComponent == null) {
            buttonComponent = GetComponent<Button>();
        }
        Interactable = false;
    }

    public void Activate() {
        CityController.Current.RegisterMoneyChangedListener(this);
        ((IMoneyChangedListener)this).onMoneyChanged(); //initialize interactable
    }

    void IMoneyChangedListener.onMoneyChanged() {
        if (CityController.Current.CurrentMoney >= buildingAttributes.Cost) {
            Interactable = true;
        } else {
            Interactable = false;
        }
    }
}
