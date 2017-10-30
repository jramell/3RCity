using System;
using UnityEngine;
using UnityEngine.UI;

public class CampaignPanel : MonoBehaviour, IMoneyChangedListener
{

    // ----------------------------------------------------------
    // Attributes and properties
    // ----------------------------------------------------------

    public Campaign campaign;

    public Text campaignTitle;
    public Text campaignCost;
    public Image campaignImage;
    public Sprite soldOutImage;

    private Button buyButton;

    // ----------------------------------------------------------
    // Methods
    // ----------------------------------------------------------

    private void Start()
    {
        CityController.Current.RegisterMoneyChangedListener(this);
        buyButton = GetComponentInChildren<Button>();
        campaignTitle.text = campaign.campaignName;
        campaignImage.sprite = campaign.logo;
        campaignCost.text = "$" + campaign.cost;
        onMoneyChanged();
    }

    public void BuyCampaign()
    {
        CityController.Current.ApplyCampaign(campaign);
        CityController.Current.RemoveMoneyChangedListener(this);
        buyButton.enabled = false;
        campaignImage.sprite = soldOutImage;
    }

    public void onMoneyChanged() {
        if (CityController.Current.CurrentMoney >= campaign.cost) {
            buyButton.interactable = true;
        } else {
            buyButton.interactable = false;
        }
    }
}
