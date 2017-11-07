using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

    static Managers instance;
    private static readonly object padlock = new object();

    private BuildingPlacement buildingPlacement;
    private PrefabManager prefabManager;
    private EventManager eventManager;

    Managers()
    {
        GameObject buildManager = GameObject.FindGameObjectWithTag("BuildManager");
        buildingPlacement = buildManager.GetComponent<BuildingPlacement>();
        prefabManager = GameObject.FindGameObjectWithTag("PrefabManager").GetComponent<PrefabManager>();
        eventManager = GameObject.FindGameObjectWithTag("EventManager").GetComponent<EventManager>();
    }

    private static Managers Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Managers();
                }
                return instance;
            }
        }
    }

    public static PrefabManager PrefabManager
    {
        get { return Instance.prefabManager; }
    }
    public static BuildingPlacement BuildingPlacementManager {
        get { return Instance.buildingPlacement; }
    }
    public static EventManager EventManager {
        get { return Instance.eventManager; }
    }
}
