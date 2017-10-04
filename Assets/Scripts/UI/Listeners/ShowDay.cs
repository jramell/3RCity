using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowDay : MonoBehaviour, IDayAdvancedListener {
    private Text dayText;

    void Start() {
        dayText = GetComponent<Text>();
        CityController.Current.RegisterDayAdvancedListener(this);
    }

    public void onDayAdvanced() {
        dayText.text = CityController.Current.CurrentDay + "d";
    }
}
