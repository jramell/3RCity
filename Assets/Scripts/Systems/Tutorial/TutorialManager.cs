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

    private string tutorialTitle;

    TutorialManager.Event CurrentEvent {
        get { return currentEvent; }
        set { currentEvent = value; }
    }

    void Start() {
        if (startWithTutorial) {
            tutorialTitle = LocalizationManager.instance.GetLocalizedValue("tutorial_intro_title");
            CurrentEvent = TutorialManager.Event.Introduction;
            Managers.EventManager.DisplayEventMessage(title: tutorialTitle, 
                                        description: LocalizationManager.instance.GetLocalizedValue("tutorial_intro_description_1"));
            Managers.EventManager.OKButton.onClick.AddListener(() => AdvanceTutorial());
        }
    }

    private void AdvanceTutorial() {
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
                Managers.EventManager.DisplayEventMessage(title: tutorialTitle, description:
                    LocalizationManager.instance.GetLocalizedValue("after_building_tutorial_dialog_1") + CityController.Current.basePayment +
                    LocalizationManager.instance.GetLocalizedValue("after_building_tutorial_dialog_2"));
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
        Managers.EventManager.DisplayEventMessage(title: tutorialTitle, description: LocalizationManager.instance.GetLocalizedValue("tutorial_objective_intro_1")
                                + CityController.Current.matchLength + LocalizationManager.instance.GetLocalizedValue("tutorial_objective_intro_2"));
        streetTrashInfoArrow.SetActive(true);
    }

    void PlayTrashOverflowIntro() {
        CurrentEvent = TutorialManager.Event.TrashOverflowIntro;
        Managers.EventManager.DisplayEventMessage(title: tutorialTitle, description: LocalizationManager.instance.GetLocalizedValue("trash_overflow_intro"));
    }

    void PlayBuildTransition() {
        CurrentEvent = TutorialManager.Event.BuildTransition;
        Managers.EventManager.DisplayEventMessage(title: tutorialTitle, description: LocalizationManager.instance.GetLocalizedValue("how_to_build_dialog"));
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

    public void onBuildingPlaced(Buildings.Type type) {
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
