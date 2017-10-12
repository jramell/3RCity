using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour, IBuildingPlacedListener {
            
    private enum Event {
        Introduction, //Tuto introduces himself
        ObjectiveIntro, //Introduces trash in streets UI
        TrashOverflowIntro, //trash overflow happens when a house's trash can or the landfill is over its max capacity
        BuildTransition, //Tuto says he's gonna show the player how to build a truck station
        BuildingIntro, //Pointing
        PointingBuildButton, 
        PointingToOrdinaryStation,
        PreviewingBuilding,
        CampaignInfo,
        Finished
    }

    [SerializeField]
    private bool startWithTutorial;

    private TutorialManager.Event currentEvent;

    [Header("Trash in streets intro Settings")]
    [SerializeField]
    private GameObject streetTrashInfoArrow;

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
    private Button closeChooseBuildingPanelButton;

    [SerializeField]
    private Button campaignsButton;

    TutorialManager.Event CurrentEvent {
        get { return currentEvent; }
        set { currentEvent = value; }
    }

    void Start() {
        if (startWithTutorial) {
            CurrentEvent = TutorialManager.Event.Introduction;
            Managers.EventManager.DisplayEventMessage(title: "Introducción", description: "¡Bienvenido, " +
                "ministro! Yo soy Tuto. Seré su ayudante mientras esté en la ciudad.");
            Managers.EventManager.OKButton.onClick.AddListener(() => AdvanceTutorial());
        }
    }

    private void AdvanceTutorial() {
        //Debug.Log("advance tutorial called with currentEvent = " + currentEvent);
        switch (CurrentEvent) {
            case TutorialManager.Event.Introduction:
                PlayObjectiveIntro(true);
                break;

            case TutorialManager.Event.ObjectiveIntro:
                PlayObjectiveIntro(false);
                PlayTrashOverflowIntro();
                break;

            case TutorialManager.Event.TrashOverflowIntro:
                PlayBuildTransition();
                break;

            case TutorialManager.Event.BuildTransition:
                PlayBuildButtonIntro();
                break;

            case TutorialManager.Event.PointingBuildButton:
                PlayBuildingIntro();
                break;

            case TutorialManager.Event.PointingToOrdinaryStation:
                CurrentEvent = TutorialManager.Event.PreviewingBuilding;
                Managers.BuildingPlacementManager.RegisterBuildingPlacedListener(this);
                ordinaryStationArrow.SetActive(false);
                ordinaryStationButton.onClick.RemoveListener(() => AdvanceTutorial());
                break;

            case TutorialManager.Event.PreviewingBuilding:
                CurrentEvent = TutorialManager.Event.CampaignInfo;
                ActivateButtons();
                Managers.EventManager.DisplayEventMessage(title: "Introducción", description:
                    "¡Bien! Esos camiones recogerán basura <color=#1ef20e>ordinaria</color> y la tirarán al vertedero. "
                        + "\n\nConstruir edificios no es barato, pero la alcaldía prometió ayudarnos con $" + CityController.Current.basePayment + " cada semana"
                        );
                break;

            case TutorialManager.Event.CampaignInfo:
                CurrentEvent = TutorialManager.Event.Finished;
                Managers.BuildingPlacementManager.RemoveBuildingPlacedListener(this);
                break;

            default:
                break;
        }
    }

    void PlayObjectiveIntro(bool play) {
        if (!play) {
            streetTrashInfoArrow.SetActive(false);
            return;
        }
        CurrentEvent = TutorialManager.Event.ObjectiveIntro;
        Managers.EventManager.DisplayEventMessage(title: "Introducción", description: "Nuestra misión es <color=#ffff00>mantener las calles limpias</color> por " + CityController.Current.matchLength + " días."
            + " \n\nEl nivel de basura en las calles está en esquina inferior izquierda de la pantalla.");
        streetTrashInfoArrow.SetActive(true);
    }

    void PlayTrashOverflowIntro() {
        CurrentEvent = TutorialManager.Event.TrashOverflowIntro;
        Managers.EventManager.DisplayEventMessage(title: "Introducción", description: "La calle se llena de basura cuando hay demasiada en la caneca de una casa o en el vertedero."
            + "\n\n<color=red>            96/80             1920/1800               </color>");
           // + "\n\nSi se llena el vertedero, no tendremos dónde más meter la basura.");
    }

    void PlayBuildTransition() {
        CurrentEvent = TutorialManager.Event.BuildTransition;
        Managers.EventManager.DisplayEventMessage(title: "Introducción", description: "Para recoger basura de las casas, necesitamos estaciones de camiones que vayan por ella."
            + "\n\nLe mostraré cómo se construye una estación.");
    }

    void PlayBuildButtonIntro() {
        CurrentEvent = TutorialManager.Event.PointingBuildButton;
        buildButtonArrow.SetActive(true);
        buildButton.onClick.AddListener(() => AdvanceTutorial());
        campaignsButton.interactable = false;
    }

    void PlayBuildingIntro() {
        CurrentEvent = TutorialManager.Event.PointingToOrdinaryStation;
        buildButton.onClick.RemoveListener(() => AdvanceTutorial());
        buildButtonArrow.SetActive(false);
        ordinaryStationArrow.SetActive(true);
        DeactivateButtons();
        ordinaryStationButton.onClick.AddListener(() => AdvanceTutorial());
    }

    public void onBuildingPlaced() {
        AdvanceTutorial();
    }

    void DeactivateButtons() {
        closeChooseBuildingPanelButton.interactable = false;
        buildButton.interactable = false;
        foreach (BuildButton button in otherBuildingsButtons) {
            button.Deactivate();
        }
        campaignsButton.interactable = false;
    }

    void ActivateButtons() {
        closeChooseBuildingPanelButton.interactable = true;
        buildButton.interactable = true;
        foreach (BuildButton button in otherBuildingsButtons) {
            button.Activate();
        }
        campaignsButton.interactable = true;
    }
}
