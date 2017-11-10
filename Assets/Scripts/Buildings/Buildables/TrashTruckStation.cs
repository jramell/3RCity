using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class TrashTruckStation : Buildable {

    public const int TRUCK_CAPACITY = 3;
    private const string ORDINARY_TC_NAME_KEY = "building_panel_ordinary_title";
    private const string PAPER_TC_NAME_KEY = "building_panel_paper_title";
    private const string GLASS_TC_NAME_KEY = "building_panel_glass_title";
    private const string METAL_TC_NAME_KEY = "building_panel_metal_title";

    [SerializeField]
    Transform trashTrucksSpawn;

    [SerializeField]
    [Tooltip("Type of garbage the trucks of this station will collect")]
    private Garbage.Type collectedGarbageType;

    List<TrashTruck> trashTrucks;

    TrashTreatmentCenter currentTreatmentCenter;
    int trashCollected = 0;
    int numberOfTrucks = 0;

    public bool paperTCBuilt;
    public bool glassTCBuilt;
    public bool metalTCBuilt;
    public bool ordinaryTCBuilt;

    void Start() {
        currentTreatmentCenter = GameObject.FindGameObjectWithTag("Controller").GetComponent<CityController>().defaultTrashTreatmentCenter;
        trashTrucks = new List<TrashTruck>(TRUCK_CAPACITY);
        buildingRenderer = transform.Find("Model").gameObject.GetComponent<Renderer>();
        originalColor = buildingRenderer.material.color;
    }

    public TrashTreatmentCenter CurrentTrashTreatmentCenter
    {
        get { return currentTreatmentCenter; }
        set { currentTreatmentCenter = value; }
    }

    public Garbage.Type CollectedGarbageType {
        get { return collectedGarbageType; }
    }

    public void AddTrashTruck(TrashTruck truck)
    {
        truck.AssignedTrashTreatmentCenter = currentTreatmentCenter;
        truck.CollectedGabargeType = collectedGarbageType;
        trashTrucks.Add(truck);
    }

    /// <summary>
    /// Colors the station green
    /// </summary>
    public override void ColorGreen() {
        buildingRenderer.material.color = Color.green;
    }

    /// <summary>
    /// Colors the station red
    /// </summary>
    public override void ColorRed() {
        buildingRenderer.material.color = Color.red;
    }

    /// <summary>
    /// Returns the station to its original color
    /// </summary>
    public override void ColorOriginal() {
        buildingRenderer.material.color = originalColor;
    }

    /// <summary>
    /// Is executed when the truck station gets actually placed in the world after previewing
    /// </summary>
    public override void Place() {
        showInfoPanel = true;
        infoDisplay = CityController.Current.centerInfoPanel;
        ColorOriginal();
        transform.Find("Model").gameObject.layer = Buildings.Layer;

        if (collectedGarbageType == Garbage.Type.Ordinary) { //if it's an ordinary station
            BeginOperations(); //spawn trucks and stuff as soon as placed
            ordinaryTCBuilt = true;
        }
        //if it's a paper station, it'll probably be a treatment center too, so...
        else if (collectedGarbageType == Garbage.Type.Paper)
        {
            //trucks of this station also deposit trash at the station, so do something like ...
            paperTCBuilt = true;
            CurrentTrashTreatmentCenter = GetComponent<PaperRecyclingCenter>();
            CityController.Current.paperCenters.Add(this); //add this center to the controller's list.
            //spaw trucks if there's a campaign active
            if (CityController.Current.PaperCampaignBought)
                BeginOperations();

        }
        else if (collectedGarbageType == Garbage.Type.Glass) {
            glassTCBuilt = true;
            CurrentTrashTreatmentCenter = GetComponent<GlassRecyclingCenter>();
            CityController.Current.glassCenters.Add(this); //add this center to the controller's list.
            //spaw trucks if there's a campaign active
            if (CityController.Current.GlassCampaignBought)
                BeginOperations();
        }
        else if (collectedGarbageType == Garbage.Type.Metal) {
            metalTCBuilt = true;
            CurrentTrashTreatmentCenter = GetComponent<MetalRecyclingCenter>();
            CityController.Current.metalCenters.Add(this); //add this center to the controller's list.
            //spaw trucks if there's a campaign active          
            if (CityController.Current.MetalCampaignBought)
                BeginOperations();
        }
        Analytics.CustomEvent("tCBuildings", new Dictionary<string, object>
        {
            { "ordinaryTC", ordinaryTCBuilt },
            { "paperTC", paperTCBuilt },
            { "metalTC", metalTCBuilt },
            { "glassTC", glassTCBuilt }
        });


    }

    public void BeginOperations() {
        GetComponent<TrashTruckStationAI>().BeginOperations();
    }

    public Vector3 TrashTruckSpawn
    {
        get { return trashTrucksSpawn.position; }
    }
    /// <summary>
    /// Executed when the mouse enters the collider.
    /// Activates the Panel that displays the house's data.
    /// </summary>
    protected override void OnMouseEnter()
    {
        if (showInfoPanel && infoDisplay != null)
        {
            string panelTitle = "";
            bool displayWarning = true;
            switch (CollectedGarbageType)
            {
                case Garbage.Type.Ordinary:
                    panelTitle = LocalizationManager.instance.GetLocalizedValue(ORDINARY_TC_NAME_KEY);
                    displayWarning = false;
                    break;
                case Garbage.Type.Paper:
                    panelTitle = LocalizationManager.instance.GetLocalizedValue(PAPER_TC_NAME_KEY);
                    displayWarning = !CityController.Current.PaperCampaignBought;
                    break;
                case Garbage.Type.Glass:
                    panelTitle = LocalizationManager.instance.GetLocalizedValue(GLASS_TC_NAME_KEY);
                    displayWarning = !CityController.Current.GlassCampaignBought;
                    break;
                case Garbage.Type.Metal:
                    panelTitle = LocalizationManager.instance.GetLocalizedValue(METAL_TC_NAME_KEY);
                    displayWarning = !CityController.Current.MetalCampaignBought;
                    break;
                default:
                    break;
            }

            infoDisplay.DisplayPanel(panelTitle, LocalizationManager.instance.GetLocalizedValue(descriptionKey), displayExtraInfo: displayWarning,
                displayOrdinary: false, ordinaryAmount: 0, ordinaryCapacityP: 0,
                displayGlass: false, glassAmount: 0, glassCapacityP: 0,
                displayMetal: false, metalAmount: 0, metalCapacityP: 0,
                displayPaper: false, paperAmount: 0, paperCapacityP: 0);
        }
    }
}
