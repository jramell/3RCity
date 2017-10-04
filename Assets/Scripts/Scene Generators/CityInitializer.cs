using UnityEngine;

public class CityInitializer : MonoBehaviour {

    [Range(0, 1000000)]
    public int startingMoney;

    [Range(0, 1000000)]
    [Tooltip("If trash in the streets is higher than this, the player will lose")]
    public int maxTrashInStreets;

    [Range(0, 1000000)]
    public int startingTrashInStreets;

    void Start() {
        CityController.Current.CurrentMoney = startingMoney;
        CityController.Current.MaxTrashInStreets = maxTrashInStreets;
        CityController.Current.TrashInStreets = startingTrashInStreets;
    }
}
