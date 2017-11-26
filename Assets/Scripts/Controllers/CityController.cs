using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene controllers. It knows the city' status and what building currently exists on it.
/// It also makes the city's date advance.
/// </summary>
public class CityController : MonoBehaviour
{

    // -----------------------------------------------------------
    // Constants
    // -----------------------------------------------------------

    /// <summary>
    /// Key used by the localization manager to know what string to put on the payment notice.
    /// </summary>
    private const string PAYMENT_NOTICE_KEY = "city_hall_payment_notice";

    // -----------------------------------------------------------
    // Attributes and properties
    // -----------------------------------------------------------

    public TrashTreatmentCenter defaultTrashTreatmentCenter;

    //
    //Panel de GameOver
    //
    public GameObject PanelGameOver;

    bool matchOver = false;

    /// <summary>
    /// The duration in game days of the current match.
    /// </summary>
    [Header("Match settings")]
    [Tooltip("The duration in game days of the current match")]
    [Range (2,1500)]
    public int matchLength;

    /// <summary>
    /// Number of days between payments to the player.
    /// </summary>
    [Tooltip("Number of days between payments to the player")]
    [Range(1, 1000)]
    public int paymentFrequency;

    /// <summary>
    /// The base amount of money given to the player on each payment.
    /// </summary>
    [Tooltip("The base amount of money given to the player on each payment.")]
    [Range(0, 1000)]
    public int basePayment;

    /// <summary>
    /// The duration in real life seconds of a day in the game.
    /// </summary>
    [Tooltip("The duration in real life seconds of a day in the game")]
    [Range(0.5f, 200)]
    public float dayDuration;

    [SerializeField]
    private int maxTrashInStreets;

    /// <summary>
    /// The current day on the game.
    /// </summary>
    [SerializeField]
    private int currentDay;

    /// <summary>
    /// List with all the houses of the city.
    /// </summary>   
    [Header("City buildings")]
    [Tooltip("List with all the houses of the city")]
    public List<House> houses;

    /// <summary>
    /// List with all the Paper reclycling Centers of the city.
    /// </summary>   
    [Tooltip("List with all the Paper reclycling Centers of the city")]
    public List<TrashTruckStation> paperCenters;

    /// <summary>
    /// List with all the Glass reclycling Centers of the city.
    /// </summary>   
    [Tooltip("List with all the Glass reclycling Centers of the city")]
    public List<TrashTruckStation> glassCenters;

    /// <summary>
    /// List with all the Metal reclycling Centers of the city.
    /// </summary>   
    [Tooltip("List with all the metal reclycling Centers of the city")]
    public List<TrashTruckStation> metalCenters;

    [Tooltip("Game Object that is displayed over others when the game is paused")]
    public GameObject pauseBackground;

    private int nextHouseToVisitIndex = -1;
    private int nextHouseToVisitOrdinaryIndex = -1;
    private int nextHouseToVisitGlassIndex = -1;
    private int nextHouseToVisitPaperIndex = -1;
    private int nextHouseToVisitMetalIndex = -1;

    /// <summary>
    /// Timer to know when to advance to the next day.
    /// </summary>
    private float timer;

    /// <summary>
    /// Counts the days since the last payment.
    /// </summary>
    private int paymentTimer;

    /// <summary>
    /// Flag to know if the game is paused or not.
    /// </summary>
    private bool paused;

    private int trashInStreets;

    private List<ITrashInStreetsChangedListener> trashInStreetsChangedListeners;

    public int MaxTrashInStreets {
        get { return maxTrashInStreets; }
        set { maxTrashInStreets = value;}
    }

    public int TrashInStreets {
        get { return trashInStreets; }
        set {
            trashInStreets = value;
            if (trashInStreetsChangedListeners == null) {
                trashInStreetsChangedListeners = new List<ITrashInStreetsChangedListener>();
            }
            if(value >= MaxTrashInStreets) {
                Paused = true;
                pauseBackground.gameObject.SetActive(false);
                textDerrota.gameObject.SetActive(true);
                Analytics.CustomEvent("DaysBeforeGameOver", new Dictionary<string, object>
                {
                    {"Days", CurrentDay }
                });
                gameOverPanel.gameObject.SetActive(true);
                matchOver = true;
            }
            foreach(ITrashInStreetsChangedListener listener in trashInStreetsChangedListeners) {
                listener.onTrashInStreetsChanged();
            }
        }
    }

    /// <summary>
    /// Property used to pause and unpause the game.
    /// </summary>
    public bool Paused
    {
        get {return  paused; }
        set
        {
            // If the paused state does not change.
            if (paused == value)
                return;

            paused = value;
            TrashTruckAI[] trucks = (TrashTruckAI[])FindObjectsOfType(typeof(TrashTruckAI));
            if (paused) {
                //Debug.Log("Paused");
                foreach (var truck in trucks) {
                    truck.Pause();
                }
                pauseBackground.SetActive(true);
            } else {
                //Debug.Log("Unpaused");
                foreach (var truck in trucks) {
                    truck.Resume();
                }
                pauseBackground.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Panel that displays a House's info
    /// </summary>
    [Tooltip("Panel that displays a House's info")]
    public DisplayGarbagePanel houseInfoPanel;

    /// <summary>
    /// Panel that displays Treatment Centers info
    /// </summary>
    [Tooltip("Panel that displays Treatment Centers info")]
    public DisplayGarbagePanel centerInfoPanel;

    /// Gameover panel, textos y estrellas
    public GameObject gameOverPanel;
    public GameObject textVictoria;
    public GameObject textDerrota;
    public GameObject OneStar;
    public GameObject TwoStar;
    public GameObject ThreeStar;

    /// <summary>
    /// Panel that displays the landfill's info
    /// </summary>
    [Tooltip("Panel that displays the landfill's info")]
    public DisplayGarbagePanel landfillInfoPanel;

    /// <summary>
    /// Informs if the Paper campaign has been bought.
    /// </summary>
    public bool PaperCampaignBought { get; set; }

    /// <summary>
    /// Informs if the Glass campaign has been bought.
    /// </summary>
    public bool GlassCampaignBought { get; set; }

    /// <summary>
    /// Informs if the Metal campaign has been bought.
    /// </summary>
    public bool MetalCampaignBought { get; set; }

    /// <summary>
    /// The current money on the city's arcs
    /// </summary>
    private int currentMoney;

    private List<IMoneyChangedListener> moneyChangedListeners;

    private List<IDayAdvancedListener> dayAdvancedListeners;

    private static CityController currentController;

    /// <summary>
    /// Property to access the city's current money.
    /// </summary>
    public int CurrentMoney
    {
        get
        {
            return currentMoney;
        }
        set
        {
            currentMoney = value;
            if (moneyChangedListeners == null) {
                moneyChangedListeners = new List<IMoneyChangedListener>();
            }
            foreach(IMoneyChangedListener listener in moneyChangedListeners) {
                listener.onMoneyChanged();
            }
        }
    }

    public int CurrentDay {
        get { return currentDay; }
    }

    private NoticePanel noticePanel;

    // -----------------------------------------------------------
    // Methods
    // -----------------------------------------------------------

    /// <summary>
    /// Executed the first frame this object is alive.
    /// </summary>
    private void Start()
    {
        currentDay = 0;
        timer = 0f;
        paymentTimer = 0;
        PaperCampaignBought = false;
        MetalCampaignBought = false;
        GlassCampaignBought = false;
        noticePanel = GameObject.FindGameObjectWithTag("Notice").GetComponent<NoticePanel>();
        noticePanel.gameObject.SetActive(false);
    }

    private void Awake()
    {
        Paused = true;
    }
    public void ReStart()
    {
        SceneManager.LoadScene("demo1");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void RegisterMoneyChangedListener(IMoneyChangedListener listener) {
        if (moneyChangedListeners == null) {
            moneyChangedListeners = new List<IMoneyChangedListener>();
        }
        moneyChangedListeners.Add(listener);
    }

    public void RemoveMoneyChangedListener(IMoneyChangedListener listener) {
        if (moneyChangedListeners == null) {
            return;
        }
        moneyChangedListeners.Remove(listener);
    }

    public void RegisterTrashInStreetsChangedListener(ITrashInStreetsChangedListener listener) {
        if (trashInStreetsChangedListeners == null) {
            trashInStreetsChangedListeners = new List<ITrashInStreetsChangedListener>();
        }
        trashInStreetsChangedListeners.Add(listener);
    }

    public void RegisterDayAdvancedListener(IDayAdvancedListener listener) {
        if (dayAdvancedListeners == null) {
            dayAdvancedListeners = new List<IDayAdvancedListener>();
        }
        dayAdvancedListeners.Add(listener);
    }

    /// <summary>
    /// Called to move forward a day on the time.
    /// </summary>
    private void AdvanceDay()
    {
        currentDay++;
        if (dayAdvancedListeners == null) {
            dayAdvancedListeners = new List<IDayAdvancedListener>();
        }
        foreach (IDayAdvancedListener listener in dayAdvancedListeners) {
            listener.onDayAdvanced();
        }
        paymentTimer++;

        // Generate garbage in all houses
        foreach (var house in houses)
        {
            house.GenerateGarbage();
        }

        if(currentDay >= matchLength && !matchOver)
        {
            // TODO: llamar al método de terminar partida
            Paused = true;
            textVictoria.gameObject.SetActive(true);
            gameOverPanel.gameObject.SetActive(true);
            Analytics.CustomEvent("TrashLevelAfterWinning", new Dictionary<string, object>
            {
                {"AmountTrash", TrashInStreets }
            });
            if (TrashInStreets <= (MaxTrashInStreets*0.333))
            {
                ThreeStar.gameObject.SetActive(true);
            }
            else if (TrashInStreets > (MaxTrashInStreets * 0.333) && TrashInStreets <= (MaxTrashInStreets * 0.666))
            {
                TwoStar.gameObject.SetActive(true);
            }
            else
            {
                OneStar.gameObject.SetActive(true);
            }
        }

        // Pay the player if enough days have passed.
        if(paymentTimer >= paymentFrequency)
        {
            paymentTimer = 0;
            PaymentToPlayer();
        }

        OrderTrashTreatment();

        Analytics.CustomEvent("builtCampaing", new Dictionary<string, object>
        {
            { "paperCamp", PaperCampaignBought },
            { "metalCamp", MetalCampaignBought },
            { "glassCamp", GlassCampaignBought }
        });
    }

    public bool BoughtCampaign(Campaigns.Type campaignType) {
        if (campaignType == Campaigns.Type.Glass) {
            Analytics.CustomEvent("timeToGlassCampaing", new Dictionary<string, object>
        {
            { "daysGlassCampaing", CurrentDay }
        });
            return GlassCampaignBought;
        }
        if (campaignType == Campaigns.Type.Metal) {
        Analytics.CustomEvent("timeToMetalCampaing", new Dictionary<string, object>
        {
            { "daysMetalCampaing", CurrentDay }
        });
            return MetalCampaignBought;
        }
        if (campaignType == Campaigns.Type.Paper) {
        Analytics.CustomEvent("timeToPaperCampaing", new Dictionary<string, object>
        {
            { "daysPaperCampaing", CurrentDay }
        });
            return PaperCampaignBought;
        }
        return false;
    }

	void Update ()
    {
        // Pause the game when the key is pressed.
        if (Input.GetKeyDown("pause") || Input.GetKeyDown("p"))
            Paused = (Paused) ? false : true;

        // Add the passed time to the timer if the game is unpaused.
        if (!Paused)
            timer += Time.deltaTime;

        // Advance day when the proper amount of time has passed.
        if (timer >= dayDuration)
        {
            AdvanceDay();
            timer = 0;
        }
	}

    /// <summary>
    /// This method works as four different independent Round Robins, one for each garbage type.
    /// </summary>
    /// <param name="truckGarbageType"></param>
    /// <returns></returns>
    public House NextHouseToCollect(Garbage.Type truckGarbageType)
    {
        int houseIndex = 0;
        switch (truckGarbageType)
        {
            case Garbage.Type.Ordinary:
                nextHouseToVisitOrdinaryIndex++;
                if (nextHouseToVisitOrdinaryIndex >= houses.Count)
                    nextHouseToVisitOrdinaryIndex = 0;
                houseIndex = nextHouseToVisitOrdinaryIndex;
                break;

            case Garbage.Type.Paper:
                nextHouseToVisitPaperIndex++;
                if (nextHouseToVisitPaperIndex >= houses.Count)
                    nextHouseToVisitPaperIndex = 0;
                houseIndex = nextHouseToVisitPaperIndex;
                break;

            case Garbage.Type.Glass:
                nextHouseToVisitGlassIndex++;
                if (nextHouseToVisitGlassIndex >= houses.Count)
                    nextHouseToVisitGlassIndex = 0;
                houseIndex = nextHouseToVisitGlassIndex;
                break;

            case Garbage.Type.Metal:
                nextHouseToVisitMetalIndex++;
                if (nextHouseToVisitMetalIndex >= houses.Count)
                    nextHouseToVisitMetalIndex = 0;
                houseIndex = nextHouseToVisitMetalIndex;
                break;

            default:
                break;
        }
        return houses[houseIndex];
    }

    /// <summary>
    /// Applies the given recycling campaign on the city's buildings.
    /// </summary>
    /// <param name="campaign"></param>
    public void ApplyCampaign(Campaign campaign)
    {
        CurrentMoney -= campaign.cost;
        switch (campaign.camapaignType)
        {
            case Campaign.CampaignType.Paper:
                PaperCampaignBought = true;
                foreach (var house in houses)
                    house.IsRecyclingPaper = true;
                foreach (var paperCenter in paperCenters)
                    paperCenter.BeginOperations();
                                        
                break;

            case Campaign.CampaignType.Glass:
                GlassCampaignBought = true;
                foreach (var house in houses)
                    house.IsRecyclingGlass = true;
                foreach (var glassCenter in glassCenters)
                    glassCenter.BeginOperations();

                break;

            case Campaign.CampaignType.Metal:
                MetalCampaignBought = true;
                foreach (var house in houses)
                    house.IsRecyclingMetal = true;
                foreach (var metalCenter in metalCenters)
                    metalCenter.BeginOperations();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// CityController component of first object tagged "Controller"
    /// </summary>
    public static CityController Current {
        get {
            if (currentController == null) {
                currentController = GameObject.FindGameObjectWithTag("Controller").GetComponent<CityController>();
            }
            return currentController;
        }
    }

    /// <summary>
    /// Pays some money to the player and displays a notice.
    /// </summary>
    public void PaymentToPlayer()
    {
        CurrentMoney += basePayment;
        //noticePanel.DisplayNotice("Has recibido $" + basePayment + " de la alcaldía");
        string localizedText = LocalizationManager.instance.GetLocalizedValue(PAYMENT_NOTICE_KEY);
        noticePanel.DisplayNotice(localizedText + basePayment);

    }

    /// <summary>
    /// Order to all trash treatment centers to treat the trash they have.
    /// </summary>
    private void OrderTrashTreatment()
    {
       foreach (var paperCenter in paperCenters)
        {
            // Get the neccesary component and call the Treat garbage method
            paperCenter.gameObject.GetComponent<TrashTreatmentCenter>().TreatGarbage();
        }

        foreach (var metalCenter in metalCenters)
        {
            // Get the neccesary component and call the Treat garbage method
            metalCenter.gameObject.GetComponent<TrashTreatmentCenter>().TreatGarbage();
        }

        foreach (var glassCenter in glassCenters)
        {
            // Get the neccesary component and call the Treat garbage method
            glassCenter.gameObject.GetComponent<TrashTreatmentCenter>().TreatGarbage();
        }

    }
}
