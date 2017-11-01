using System.Collections.Generic;
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
    private List<ICampaignBoughtListener> campaignBoughtListeners;

    // ----------------------------------------------------------
    // Methods
    // ----------------------------------------------------------

    private void Start()
    {
        CityController.Current.RegisterMoneyChangedListener(this);
        if(campaignBoughtListeners == null) {
            campaignBoughtListeners = new List<ICampaignBoughtListener>();
        }
        buyButton = GetComponentInChildren<Button>();
        campaignTitle.text = campaign.campaignName;
        campaignImage.sprite = campaign.logo;
        campaignCost.text = "$" + campaign.cost;
        onMoneyChanged();
    }

    public void RegisterCampaignBoughtListener(ICampaignBoughtListener listener) {
        if (campaignBoughtListeners == null) {
            campaignBoughtListeners = new List<ICampaignBoughtListener>();
        }
        campaignBoughtListeners.Add(listener);
    }

    public void BuyCampaign()
    {
        foreach(ICampaignBoughtListener listener in campaignBoughtListeners) {
            listener.onCampaignBought();
        }
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
