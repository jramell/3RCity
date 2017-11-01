using System.Collections;
using UnityEngine;

public class IncompleteRecyclingHandler : MonoBehaviour, IBuildingPlacedListener {

    public float secondsBeforeWarning = 12f;

    [Tooltip("How many times should Tuto remind the player to buy the appropriate campaign?")]
    public int timesToRemind = 1;

    bool campaignActive = false;
    int timesReminded = 0;

    Campaigns.Type typeOfCampaignLookingFor;

    public void onBuildingPlaced(Buildings.Type buildingType) {
        if (timesReminded == timesToRemind) {
            return;
        }
        if (campaignActive) {
            return;
        }
        if (buildingType == Buildings.Type.MetalRecyclingCenter) {
            typeOfCampaignLookingFor = Campaigns.Type.Metal;
        } else if (buildingType == Buildings.Type.GlassRecyclingCenter) {
            typeOfCampaignLookingFor = Campaigns.Type.Glass;
        } else if (buildingType == Buildings.Type.PaperRecyclingCenter) {
            typeOfCampaignLookingFor = Campaigns.Type.Paper;
        } else {
            return;
        }
        if (!CityController.Current.BoughtCampaign(typeOfCampaignLookingFor)) {
            StartCoroutine(buildingPlacedWithoutCampaign());
        }
    }

    IEnumerator buildingPlacedWithoutCampaign() {
        campaignActive = true;
        timesReminded++;
        yield return new WaitForSeconds(secondsBeforeWarning);
        if (!CityController.Current.BoughtCampaign(typeOfCampaignLookingFor)) {
            Managers.EventManager.DisplayEventMessage(title: "Campañas de reciclaje",
                description: "Ministro, veo que compró un centro de reciclaje, pero no la campaña que necesita para que funcione."
                + "\n\nRecuerde que para reciclar metal, necesita comprar la campaña de reciclaje de metal, y así para cada centro de reciclaje.");
        }
        campaignActive = false;
    }

    void Start () {
        Managers.BuildingPlacementManager.RegisterBuildingPlacedListener(this);
	}
}
