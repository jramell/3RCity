using System.Collections;
using UnityEngine;

public class IncompleteRecyclingHandler : MonoBehaviour, IBuildingPlacedListener {

    [Tooltip("Seconds Tuto will wait before telling player to buy appropriate campaign")]
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
            Managers.EventManager.DisplayEventMessage(title: LocalizationManager.instance.GetLocalizedValue("buy_recycling_campaign_reminder_title"),
                description: LocalizationManager.instance.GetLocalizedValue("buy_recycling_campaign_reminder_description"));
        }
        campaignActive = false;
    }

    void Start () {
        Managers.BuildingPlacementManager.RegisterBuildingPlacedListener(this);
	}
}
