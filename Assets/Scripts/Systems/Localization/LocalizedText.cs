using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour {

    public string key;

	void Start () {
        Text attachedText = GetComponent<Text>();
        string localizedText = LocalizationManager.instance.GetLocalizedValue(key);
        if (localizedText != LocalizationManager.KEY_NOT_FOUND) {
            attachedText.text = localizedText;
        }
	}
}
