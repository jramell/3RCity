using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour, IBuildingPlacedListener {
            
    private enum Event {
        Introduction, 
        PointingBuildButton,
        PointingToOrdinaryStation,
        PreviewingBuilding,
        CampaignInfo,
        Finished
    }

    [SerializeField]
    private bool startWithTutorial;

    private TutorialManager.Event currentEvent;

    [Header("Building intro Settings")]
    [SerializeField]
    private GameObject buildButtonArrow;

    [SerializeField]
    private GameObject ordinaryStationArrow;

    [SerializeField]
    private Button buildButton;

    [SerializeField]
    private Button ordinaryStationButton;

    [SerializeField]
    private List<BuildButton> otherBuildingsButtons;

    [SerializeField]
    private Button campaignsButton;

    void Start() {
        if (startWithTutorial) {
            currentEvent = TutorialManager.Event.Introduction;
            Managers.EventManager.DisplayEventMessage(title: "Introducción", description: "Ministro, hay " +
                " que evitar que la basura se acumule en esta ciudad. ¡Todos cuentan con usted!" +
                "\n \nComencemos construyendo una estación de recolección de basura ordinaria."
                );
            Managers.EventManager.OKButton.onClick.AddListener(() => AdvanceTutorial());
        }
    }

    private void AdvanceTutorial() {
        switch (currentEvent) {
            case TutorialManager.Event.Introduction:
                currentEvent = TutorialManager.Event.PointingBuildButton;
                buildButtonArrow.SetActive(true);
                buildButton.onClick.AddListener(() => AdvanceTutorial());
                break;

            case TutorialManager.Event.PointingBuildButton:
                currentEvent = TutorialManager.Event.PointingToOrdinaryStation;
                buildButton.onClick.RemoveListener(() => AdvanceTutorial());
                buildButtonArrow.SetActive(false);
                ordinaryStationArrow.SetActive(true);
                DeactivateButtons();
                ordinaryStationButton.onClick.AddListener(() => AdvanceTutorial());
                break;

            case TutorialManager.Event.PointingToOrdinaryStation:
                currentEvent = TutorialManager.Event.PreviewingBuilding;
                Managers.BuildingPlacementManager.RegisterBuildingPlacedListener(this);
                ordinaryStationArrow.SetActive(false);
                ordinaryStationButton.onClick.RemoveListener(() => AdvanceTutorial());
                break;

            case TutorialManager.Event.PreviewingBuilding:
                currentEvent = TutorialManager.Event.CampaignInfo;
                ActivateButtons();
                Managers.EventManager.DisplayEventMessage(title: "Introducción", description: 
                    "¡Bien! Los camiones se encargarán del resto. Eso sí, si se acumula la basura en las casas o se"
                        + " llena el vertedero... Nos va mal. \n\nPara evitarlo, podemos... ¿Hacer campañas de reciclaje o algo así?" +
                        " No sé. Lo dejo en sus manos, ministro.");
                break;

            case TutorialManager.Event.CampaignInfo:
                currentEvent = TutorialManager.Event.Finished;
                Managers.BuildingPlacementManager.RemoveBuildingPlacedListener(this);
                break;

            default:
                break;
        }
    }

    void DeactivateButtons() {
        buildButton.interactable = false;
        foreach (BuildButton button in otherBuildingsButtons) {
            button.Deactivate();
        }
        campaignsButton.interactable = false;
    }

    void ActivateButtons() {
        buildButton.interactable = true;
        foreach (BuildButton button in otherBuildingsButtons) {
            button.Activate();
        }
        campaignsButton.interactable = true;
    }

    public void onBuildingPlaced() {
        AdvanceTutorial();
    }
}
