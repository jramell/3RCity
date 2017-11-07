using UnityEngine;

public class PrefabManager : MonoBehaviour {

    [Header("Buildables")]

    [SerializeField]
    private GameObject trashTruckStationPrefab;

    [SerializeField]
    private GameObject paperRecyclingCenterPrefab;

    [SerializeField]
    private GameObject glassRecyclingCenterPrefab;

    [SerializeField]
    private GameObject metalRecyclingCenterPrefab;

    [Header("Trucks")]

    [SerializeField]
    private GameObject regularTrashTruck;

    [SerializeField]
    private GameObject paperTrashTruck;

    [SerializeField]
    private GameObject glassTrashTruck;

    [SerializeField]
    private GameObject metalTrashTruck;

    public GameObject MapBuildingToPrefab(Buildings.Type building)
    {
        GameObject answer = null;
        if (building == Buildings.Type.TrashTruckStation) {
            answer = trashTruckStationPrefab;
        }
        else if (building == Buildings.Type.PaperRecyclingCenter) {
            answer = paperRecyclingCenterPrefab;
        } 
        else if (building == Buildings.Type.GlassRecyclingCenter) {
            answer = glassRecyclingCenterPrefab;
        } 
        else if (building == Buildings.Type.MetalRecyclingCenter) {
            answer = metalRecyclingCenterPrefab;
        }
        return answer;
    }

    public GameObject TrashTruckPrefabOfType(Garbage.Type trashTruckType)
    {
        GameObject answer = regularTrashTruck;
        if (trashTruckType == Garbage.Type.Paper) {
            answer = paperTrashTruck;
        }
        else if (trashTruckType == Garbage.Type.Glass) {
            answer = glassTrashTruck;
        }
        else if (trashTruckType == Garbage.Type.Metal) {
            answer = metalTrashTruck;
        }
        return answer;
    }
}
