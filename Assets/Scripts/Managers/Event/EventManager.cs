using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    [SerializeField]
    private GameObject eventPanel;

    /// <summary>
    /// Text with the event description
    /// </summary>
    [SerializeField]
    [Tooltip("Text with the event description")]
    private Text titleText;

    [SerializeField]
    private Text descriptionText;

    [SerializeField]
    private Button okButton;

    public Button OKButton {
        get { return okButton; }
    }

    public void DisplayEventMessage(string title, string description) {
        CityController.Current.Paused = true;
        titleText.text = title;
        descriptionText.text = description;
        okButton.onClick.AddListener( () => HideEvent() );
        eventPanel.SetActive(true);
    }
    
    public void HideEvent() {
        eventPanel.SetActive(false);
        CityController.Current.Paused = false;
    }
}
