using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Swaps the text in the Text Component with the localized text identified with the entered key
/// </summary>
[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour {

    /// <summary>
    /// Key the localized text is registered with in the loaded localization file
    /// </summary>
    [Tooltip("Key the localized text is registered with in the loaded localization file")]
    public string key;

	void Start () {
        Text attachedText = GetComponent<Text>();
        string localizedText = LocalizationManager.instance.GetLocalizedValue(key);
        if (localizedText != LocalizationManager.KEY_NOT_FOUND) {
            attachedText.text = localizedText;
        }
	}
}
