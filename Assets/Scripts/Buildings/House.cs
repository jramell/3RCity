using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// Script for a single house. A house generates garbage over time.
/// </summary>
[RequireComponent(typeof(Collider))]
public class House : MonoBehaviour
{
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
            // TODO: do something else if neccesary, like updating the model if 
            // the current amount if higher than the capacity, etc.
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
            // TODO: do something else if neccesary, like updating the model if 
            // the current amount if higher than the capacity, etc.
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
            // TODO: do something else if neccesary, like updating the model if 
            // the current amount if higher than the capacity, etc.
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
            // TODO: do something else if neccesary, like updating the model if 
            // the current amount if higher than the capacity, etc.
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
	public GameObject ordinaryTrashBag;

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
            // Instantiate paper bag
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
            // Instanciate paper bag
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
            // Instanciate paper bag
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
            garbageNames.Add( "papel");
        }

        if (IsRecyclingMetal)
        {
            recyclingCansCounter++;
            garbageNames.Add("metal");
        }

        if (IsRecyclingGlass)
        {
            recyclingCansCounter++;
            garbageNames.Add("vidrio");
        }

        switch (recyclingCansCounter)
        {
            case 0:
                message = "Esta casa no recicla.";
                break;

            case 1:
                message = "Esta casa solo recicla " + garbageNames[0] + ".";
                break;

            case 2:
                message = "Esta casa recicla " + garbageNames[0] + " y " + garbageNames[1] + ".";
                break;

            case 3:
                message = "Esta casa recicla todo tipo de desechos.";
                break;

            default:
                break;
        }

        return message;
    }

   

}
