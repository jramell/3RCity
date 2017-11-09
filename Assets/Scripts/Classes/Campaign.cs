using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign : MonoBehaviour
{

    // --------------------------------------------------------
    // Constants
    // --------------------------------------------------------

    /// <summary>
    /// The campaign types.
    /// </summary>
    public enum CampaignType
    {
        Paper,
        Glass,
        Metal
    }

    // --------------------------------------------------------
    // Attributes
    // --------------------------------------------------------

    /// <summary>
    /// This campaign's type.
    /// </summary>
    public CampaignType camapaignType;

    /// <summary>
    /// The monetary cost of this campaing
    /// </summary>
    [Range(0,1000)]
    [Tooltip("The monetary cost of this campaing.")]
    public int cost;

    /// <summary>
    /// Localization Manager key for the name that's going to be displayed on the UI.
    /// </summary>
    [Tooltip("Localization Manager key for the name that's going to be displayed on the UI.")]
    public string campaignNameKey;

    /// <summary>
    /// Short text that's going to be displayed on the UI.
    /// </summary>
    [Tooltip("Short description that's going to be displayed on the UI.")]
    public string description;

    /// <summary>
    /// Camapign Logo
    /// </summary>
    [Tooltip("Short description that's going to be displayed on the UI.")]
    public Sprite logo;
}
