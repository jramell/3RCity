using UnityEngine;
using UnityEngine.UI;

public class ShowTrashInStreets : MonoBehaviour, ITrashInStreetsChangedListener {

    private Text trashText;


    void Start () {
        CityController.Current.RegisterTrashInStreetsChangedListener(this);
        trashText = GetComponent<Text>();
        onTrashInStreetsChanged();
	}

    public void onTrashInStreetsChanged() {
        trashText.text = CityController.Current.TrashInStreets + "/" + CityController.Current.MaxTrashInStreets;
    }
}
