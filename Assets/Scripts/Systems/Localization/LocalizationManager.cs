using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour {

    /// <summary>
    /// Returned when a localization key that tried to be retrieved wasn't found
    /// </summary>
    public const string KEY_NOT_FOUND = "";

    public static LocalizationManager instance;

    /// <summary>
    /// Contains all localization keys and their values in the selected language
    /// </summary>
    private Dictionary<string, string> localizedText;

    private bool finishedLoading = false;

	void Awake() {
		if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
	}

    /// <summary>
    /// Reads file with localized text as key,value pairs and initializes the localizedText dictionary with it.
    /// </summary>
    /// <param name="fileName">path of the localized text asset</param>
    public void LoadLocalizedText(string fileName) {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (!File.Exists(filePath)) {
            Debug.Log("Cannot find Localization File at path " + filePath);
        }
        string dataAsJSON = File.ReadAllText(filePath);
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);
        for(int i = 0; i < loadedData.items.Length; i++) {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        finishedLoading = true;
    }

    /// <summary>
    /// If the key passed as a parameter is found in the localized text dictionary, returns the localized
    /// text in the currently loaded language. Else, returns LocaizationManager.KEY_NOT_FOUND
    /// </summary>
    /// <param name="key">key of the localized text to retrieve</param>
    /// <returns>The key's localized text value if the key is found, KEY_NOT_FOUND if it's not.</returns>
    public string GetLocalizedValue(string key) {
        if(!localizedText.ContainsKey(key)) {
            return KEY_NOT_FOUND;
        }
        return localizedText[key];
    }

    public bool FinishedLoading {
        get { return finishedLoading; }
    }
}
