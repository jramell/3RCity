﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Script for a single house. A house generates garbage over time.
/// </summary>
[RequireComponent(typeof(Collider))]
public class House : MonoBehaviour
{

    // ------------------------------------------------------------
    // Constants
    // ------------------------------------------------------------

    /// <summary>
    /// The height where the can content objects should be when the cans are almost empty.
    /// </summary>
    private const float CanContentLowPosition = 0.27f;

    /// <summary>
    /// The height where the can content objects should be when the cans are halfway full.
    /// </summary>
    private const float CanContentMidPosition = 0.45f;

    /// <summary>
    /// The height where the can content objects shouls be when the cans are practically full.
    /// </summary>
    private const float CanContentHighPosition = 0.92f;

    private const string DOES_NOT_RECYCLE_KEY = "house_panel_does_not_recycle";
    private const string RECYCLES_1_KEY = "house_panel_recycles_1";
    private const string RECYCLES_2_KEY = "house_panel_recycles_2";
    private const string RECYCLES_ALL_KEY = "house_panel_recycles_all";
    private const string AND_KEY = "house_panel_and";

    // ------------------------------------------------------------
    // Attributes and properties
    // ------------------------------------------------------------

    /// <summary>
    /// Ordinary trash can's maximum capacity. 
    /// All recyclable garbage that is not currently being recycled goes here.
    /// </summary>
    [Header("Trash Cans Settings")]
    [Tooltip("Ordinary trash can's maximum capacity")]
    [SerializeField]
    [Range(1,100)]
    private int ordinaryCanCapacity;

    private TrashCan ordinaryTrashCan;

    /// <summary>
    /// The house's trash can for ordinary and unclassified trash
    /// </summary>
    public TrashCan OrdinaryTrashCan
    {
        get { return ordinaryTrashCan; }
        set
        {
            ordinaryTrashCan = value;                       
        }
    }

    /// <summary>
    /// Paper trash can's maximum capacity. 
    /// </summary>
    [Tooltip("Paper  can's maximum capacity")]
    [Range(1, 100)]
    public int paperCanCapacity;

    /// <summary>
    /// The house's trash can for paper
    /// </summary>
    private TrashCan paperTrashCan;

    /// <summary>
    /// The house's trash can for paper
    /// </summary>
    public TrashCan PaperTrashCan
    {
        get { return paperTrashCan; }
        set
        {
            paperTrashCan = value;
        }
    }

    /// <summary>
    /// Glass trash can's maximum capacity. 
    /// </summary>
    [Tooltip("Glass can's maximum capacity")]
    [Range(1, 100)]
    public int glassCanCapacity;

    /// <summary>
    /// The house's trash can for glass
    /// </summary>
    private TrashCan glassTrashCan;

    /// <summary>
    /// Property to access the current ammount of garbage in the glass trash can.
    /// Only recycled glass goes here.
    /// </summary>
    public TrashCan GlassTrashCan
    {
        get { return glassTrashCan; }
        set
        {
            glassTrashCan = value;
        }
    }

    /// <summary>
    /// Metal trash can's maximum capacity. 
    /// </summary>
    [Tooltip("Metal can's maximum capacity")]
    [Range(1, 100)]
    public int metalCanCapacity;

    /// <summary>
    /// The house's trash can for metal
    /// </summary>
    private TrashCan metalTrashCan;

    /// <summary>
    /// The house's trash can for metal
    /// </summary>
    public TrashCan MetalTrashCan
    {
        get { return metalTrashCan; }
        set
        {
            metalTrashCan = value;
        }
    }

    /// <summary>
    /// The minimun of ordinary garbage that can be generated daily
    /// </summary>
    [Header("Garbage generation")]
    [Tooltip("The minimun of ordinary garbage that can be generated daily")]
    public int ordinaryMinimunGeneration;

    /// <summary>
    /// The maximun of ordinary garbage that can be generated daily
    /// </summary>
    [Tooltip("The maximun of ordinary garbage that can be generated daily")]
    public int ordinaryMaximunGeneration;

    /// <summary>
    /// The minimun of paper that can be generated daily
    /// </summary>
    [Tooltip("The minimun of paper that can be generated daily")]
    public int paperMinimunGeneration;

    /// <summary>
    /// The maximun of paper that can be generated daily
    /// </summary>
    [Tooltip("The maximum of paper garbage that can be generated daily")]
    public int paperMaximunGeneration;

    /// <summary>
    /// The minimun of paper that can be generated daily
    /// </summary>
    [Tooltip("The minimun of glass that can be generated daily")]
    public int glassMinimunGeneration;

    /// <summary>
    /// The maximun of paper that can be generated daily
    /// </summary>
    [Tooltip("The maximum of glass garbage that can be generated daily")]
    public int glassMaximunGeneration;

    /// <summary>
    /// The minimun of metal that can be generated daily
    /// </summary>
    [Tooltip("The minimun of metal that can be generated daily")]
    public int metalMinimunGeneration;

    /// <summary>
    /// The maximun of metal that can be generated daily
    /// </summary>
    [Tooltip("The maximum of metal garbage that can be generated daily")]
    public int metalMaximunGeneration;

    /// <summary>
    /// The transform of the place where the trucks stop to collect the garbage. 
    /// </summary>
    [Header("Trash cans extra settings")]
    [Tooltip("The transform of the place where the trucks stop to collect the garbage.")]
    public Transform trashCanTrasnform;

    /// <summary>
    /// Position of the trash can
    /// </summary>
    public Transform TrashCan
    {
        get { return trashCanTrasnform; }
    }

    /// <summary>
    /// Prefab of the garbage bag model thet is displayed when the House generates garbage.
    /// </summary>
    [Tooltip("Prefab of the garbage bag model thet is displayed when the House generates garbage.")]
    public GameObject ordinaryTrashBag;

    /// <summary>
    /// 3D model of the content of the can. It gets higher when the amount of garbage increases. 
    /// </summary>
    [Tooltip("3D model of the content of the can.")]
    public GameObject ordinaryCanContent;

    /// <summary>
    /// 3D model of the content of the can. It gets higher when the amount of garbage increases. 
    /// </summary>
    [Tooltip("3D model of the content of the can.")]
    public GameObject paperCanContent;

    /// <summary>
    /// 3D model of the content of the can. It gets higher when the amount of garbage increases. 
    /// </summary>
    [Tooltip("3D model of the content of the can.")]
    public GameObject glassCanContent;

    /// <summary>
    /// 3D model of the content of the can. It gets higher when the amount of garbage increases. 
    /// </summary>
    [Tooltip("3D model of the content of the can.")]
    public GameObject metalCanContent;

    /// <summary>
    /// 3D model of the ordinary garbage that appears when the can is overflowed.
    /// This is displayed when there's little thrash on the streets.
    /// </summary>
    [Tooltip("3D model of the ordinary garbage that appears when the can is overflowed")]
    public GameObject ordinaryStreetTrashLow;

    /// <summary>
    /// 3D model of the paper garbage that appears when the can is overflowed.
    /// This is displayed when there's little thrash on the streets.
    /// </summary>
    [Tooltip("3D model of the paper garbage that appears when the can is overflowed")]
    public GameObject paperStreetTrashLow;

    /// <summary>
    /// 3D model of the glass garbagethat appears when the can is overflowed.
    /// This is displayed when there's little thrash on the streets.
    /// </summary>
    [Tooltip("3D model of the glass garbage that appears when the can is overflowed")]
    public GameObject glassStreetTrashLow;

    /// <summary>
    /// 3D model of the metal garbage that appears when the can is overflowed.
    /// This is displayed when there's little thrash on the streets.
    /// </summary>
    [Tooltip("3D model of the metal garbage that appears when the can is overflowed")]
    public GameObject metalStreetTrashLow;



    /// <summary>
    /// Reference to the ordinary can transform. Is used to know where to place the garbage bags.
    /// </summary>
    private Transform ordinaryCanTransform;

    /// <summary>
    /// True if the house is recycling paper
    /// </summary>
    private bool isRecyclingPaper; 

    /// <summary>
    /// True if the house is recycling paper
    /// </summary>
    public bool IsRecyclingPaper
    {
        get { return isRecyclingPaper; }
        set
        {
            isRecyclingPaper = value;
            PaperCanObject.SetActive(value);          
        }
    }

    /// <summary>
    /// True if the house is recycling metal
    /// </summary>
    private bool isRecyclingMetal;

    /// <summary>
    /// True if the house is recycling metal.
    /// </summary>
    public bool IsRecyclingMetal
    {
        get { return isRecyclingMetal; }
        set
        {
            isRecyclingMetal = value;
            MetalCanObject.SetActive(value);
        }
    }

    /// <summary>
    /// True if the house is recycling glass.
    /// </summary>
    private bool isRecyclingGlass;

    /// <summary>
    /// True if the house is recycling glass.
    /// </summary>
    public bool IsRecyclingGlass
    {
        get { return isRecyclingGlass; }
        set
        {
            isRecyclingGlass = value;
            GlassCanObject.SetActive(value);
        }
    }

    /// <summary>
    /// Reference to the controller of the scene.
    /// </summary>
    private CityController controller;

    /// <summary>
    /// Reference to the UI panel tha diplays info about the house.
    /// </summary>
    private DisplayGarbagePanel houseInfoDisplay;

    /// <summary>
    /// The Object with the Paper can model.
    /// </summary>
    private GameObject PaperCanObject;

    /// <summary>
    /// The Object with the Glass can model.
    /// </summary>
    private GameObject GlassCanObject;

    /// <summary>
    /// The Object with the Metal can model.
    /// </summary>
    private GameObject MetalCanObject;

 
    // ------------------------------------------------------------
    // Methods
    // ------------------------------------------------------------

    void Start ()
    {
        InitializeTrashCans();
		ordinaryCanTransform = transform.GetChild (0); // 0 is the ordinary can
        PaperCanObject = transform.GetChild(1).gameObject; // 1 is the paper can
        MetalCanObject = transform.GetChild(2).gameObject; // 2 is the metal can
        GlassCanObject = transform.GetChild(3).gameObject; // 3 is the glass can
        IsRecyclingGlass = false;
        IsRecyclingMetal = false;
        IsRecyclingPaper = false;
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<CityController>();
        houseInfoDisplay = controller.houseInfoPanel;
    }

    private void InitializeTrashCans() {
        ordinaryTrashCan = new TrashCan(Garbage.Type.Ordinary, ordinaryCanCapacity);
        paperTrashCan = new TrashCan(Garbage.Type.Paper, paperCanCapacity);
        glassTrashCan = new TrashCan(Garbage.Type.Glass, glassCanCapacity);
        metalTrashCan = new TrashCan(Garbage.Type.Metal, metalCanCapacity);
    }
	
    /// <summary>
    /// Generates new garbage and add it to the current amount.
    /// It puts each type of garbage in the proper can, but only if the house is recycling.
    /// </summary>
    public void GenerateGarbage()
    {
        int amount = 0;
        int paperRec = 0;
        int glassRec = 0;
        int metalRec = 0;
        // Ordinary garbage
        amount = Random.Range(ordinaryMinimunGeneration, ordinaryMaximunGeneration);
        ordinaryTrashCan.DepositTrash(amount);
		Vector3 bagPosition = ordinaryCanTransform.position;
		bagPosition.y += 1f;
		bagPosition.x += -0.166f; 
		bagPosition.z += -0.7481f;
		GameObject instance = Instantiate(ordinaryTrashBag, bagPosition, Quaternion.identity, transform);
        UpdateCanFilling(Garbage.Type.Ordinary);

        // Paper  
        amount = Random.Range(paperMinimunGeneration, paperMaximunGeneration);
        if (IsRecyclingPaper)
        {
            paperTrashCan.DepositTrash(amount);
            paperRec = amount;
            Vector3 bagPositionPaper = PaperCanObject.transform.position;
            bagPositionPaper.y += 1f;
            bagPositionPaper.x += -0.166f;
            bagPositionPaper.z += -0.551f;
            GameObject instancePaperB = Instantiate(ordinaryTrashBag, bagPositionPaper, Quaternion.identity, transform);
            UpdateCanFilling(Garbage.Type.Paper);

        } else {
            ordinaryTrashCan.DepositTrash(amount);
        }

        // glass 
        amount = Random.Range(glassMinimunGeneration, glassMaximunGeneration);
        if (IsRecyclingGlass)
        {
            glassTrashCan.DepositTrash(amount);
            glassRec = amount;
            Vector3 bagPositionGlass = PaperCanObject.transform.position;
            bagPositionGlass.y += 1f;
            bagPositionGlass.x += -0.066f;
            bagPositionGlass.z += 0.475f; 
            GameObject instanceGlassB = Instantiate(ordinaryTrashBag, bagPositionGlass, Quaternion.identity, transform);
            UpdateCanFilling(Garbage.Type.Glass);

        } else {
            ordinaryTrashCan.DepositTrash(amount);
        }

        // metal 
        amount = Random.Range(metalMinimunGeneration, metalMaximunGeneration);
        if (IsRecyclingMetal)
        {
            metalTrashCan.DepositTrash(amount);
            metalRec = amount;
            Vector3 bagPositionMetal = MetalCanObject.transform.position;
            bagPositionMetal.y += 1f;
            bagPositionMetal.x += -0.166f;
            bagPositionMetal.z += -0.7481f;
            GameObject instanceMetalB = Instantiate(ordinaryTrashBag, bagPositionMetal, Quaternion.identity, transform);
            UpdateCanFilling(Garbage.Type.Metal);
        }
        else {
            ordinaryTrashCan.DepositTrash(amount);
        }
        Analytics.CustomEvent("basuraCE", new Dictionary<string, object>
        {
            { "TrashBags", amount },
            { "Paper", paperRec },
            { "Metal", metalRec },
            { "Glass", glassRec },
            { "Ordinary", 0 }
        });
    }

    /// <summary>
    /// Executed when the mouse enters the collider.
    /// Activates the Panel that displays the house's data.
    /// </summary>
    private void OnMouseEnter()
    {
        if (CityController.Current == null || CityController.Current.Paused)
            return;
        houseInfoDisplay.DisplayPanel( FindExtraInfoMessage(),
            displayOrdinary: true, ordinaryAmount: ordinaryTrashCan.CurrentAmount, ordinaryCapacityP: ordinaryCanCapacity, 
            displayGlass: IsRecyclingGlass, glassAmount: glassTrashCan.CurrentAmount, glassCapacityP: glassCanCapacity,
            displayMetal: IsRecyclingMetal, metalAmount: metalTrashCan.CurrentAmount, metalCapacityP: metalCanCapacity,
            displayPaper: IsRecyclingPaper, paperAmount: paperTrashCan.CurrentAmount, paperCapacityP: paperCanCapacity);      
    }

    /// <summary>
    /// Executed each frame the mouse is over the collider.
    /// Updates the info panel's data.
    /// </summary>
    private void OnMouseOver()
    {
        if (CityController.Current == null || CityController.Current.Paused)
            return;
        houseInfoDisplay.UpdateValues(
            ordinaryAmount: ordinaryTrashCan.CurrentAmount,
            glassAmount: glassTrashCan.CurrentAmount,
            metalAmount: metalTrashCan.CurrentAmount,
            paperAmount: paperTrashCan.CurrentAmount);
    }

    /// <summary>
    /// Executed when the mouse leaves the collider.
    /// Hides the panel that displays the House's data.
    /// </summary>
    private void OnMouseExit()
    {
        if (CityController.Current == null || CityController.Current.Paused)
            return;
        houseInfoDisplay.gameObject.SetActive(false);
    }

    /// <summary>
    /// Returns the message that should be displayed on the House info panel.
    /// </summary>
    /// <returns></returns>
    private string FindExtraInfoMessage()
    {
        string message = "";
        int recyclingCansCounter = 0;
        List<string> garbageNames = new List<string>();

        if(IsRecyclingPaper)
        {
            recyclingCansCounter++;
            //garbageNames.Add( "papel");
            garbageNames.Add(LocalizationManager.instance.GetLocalizedValue("paper"));
        }

        if (IsRecyclingMetal)
        {
            recyclingCansCounter++;
            //garbageNames.Add("metal");
            garbageNames.Add(LocalizationManager.instance.GetLocalizedValue("metal"));
        }

        if (IsRecyclingGlass)
        {
            recyclingCansCounter++;
            //garbageNames.Add("vidrio");
            garbageNames.Add(LocalizationManager.instance.GetLocalizedValue("glass"));
        }

        switch (recyclingCansCounter)
        {
            case 0:
                //message = "Esta casa no recicla.";
                message = LocalizationManager.instance.GetLocalizedValue(DOES_NOT_RECYCLE_KEY);
                break;

            case 1:
                //message = "Esta casa solo recicla " + garbageNames[0] + ".";
                message = LocalizationManager.instance.GetLocalizedValue(RECYCLES_1_KEY);
                message += garbageNames[0] + ".";
                break;

            case 2:
                //message = "Esta casa recicla " + garbageNames[0] + " y " + garbageNames[1] + ".";
                message = LocalizationManager.instance.GetLocalizedValue(RECYCLES_2_KEY);
                message += garbageNames[0] + " " + LocalizationManager.instance.GetLocalizedValue(AND_KEY);
                message += " " + garbageNames[1] + ".";
                break;

            case 3:
                //message = "Esta casa recicla todo tipo de desechos.";
                message = LocalizationManager.instance.GetLocalizedValue(RECYCLES_ALL_KEY);
                break;

            default:
                break;
        }

        return message;
    }

    public void UpdateCanFilling(Garbage.Type garbageType)
    {
        float fillRate = 0;
        GameObject canFilling = null;
        GameObject streetThrash = null;

        switch (garbageType)
        {
            case Garbage.Type.Ordinary:
                fillRate = (float)OrdinaryTrashCan.CurrentAmount / (float)ordinaryCanCapacity;
                canFilling = ordinaryCanContent;
                streetThrash = ordinaryStreetTrashLow;
                break;
            case Garbage.Type.Paper:
                fillRate = (float)PaperTrashCan.CurrentAmount / (float)paperCanCapacity;
                canFilling = paperCanContent;
                streetThrash = paperStreetTrashLow;
                break;
            case Garbage.Type.Glass:
                fillRate = (float)GlassTrashCan.CurrentAmount / (float)glassCanCapacity;
                canFilling = glassCanContent;
                streetThrash = glassStreetTrashLow;
                break;
            case Garbage.Type.Metal:
                fillRate = (float)MetalTrashCan.CurrentAmount / (float)metalCanCapacity;
                canFilling = metalCanContent;
                streetThrash = metalStreetTrashLow;
                break;
            default:
                break;
        }


        float height = 0;
        canFilling.SetActive(true);

        if (fillRate < 0.1f)
        {
            canFilling.SetActive(false);
        }
        else if (fillRate >= 0.1f && fillRate < 0.4f)
        {
            height = CanContentLowPosition;
        }
        else if (fillRate >= 0.4f && fillRate < 0.75f)
        {
            height = CanContentMidPosition;
        }
        else if ( fillRate >= 0.75f)
        {
            height = CanContentHighPosition;
        }

        if (fillRate > 1)
        {
            streetThrash.gameObject.SetActive(true);
        }
        else
        {
            streetThrash.gameObject.SetActive(false);
        }

        Vector3 fillPosition = canFilling.transform.position;
        fillPosition.y = height;
        canFilling.transform.position = fillPosition;

    }

   

}
