using System;
using UnityEngine;
using UnityEngine.UI;

public class FillTrashInStreetsIcon : MonoBehaviour, ITrashInStreetsChangedListener {

    private Image hazardIcon;

	void Start () {
        hazardIcon = GetComponent<Image>();
        CityController.Current.RegisterTrashInStreetsChangedListener(this);
        onTrashInStreetsChanged();
	}

    public void onTrashInStreetsChanged() {
        hazardIcon.fillAmount = (float) ((float)CityController.Current.TrashInStreets/(float)CityController.Current.MaxTrashInStreets);
        hazardIcon.color = Color.Lerp(Color.green, Color.red, hazardIcon.fillAmount);
    }
}
