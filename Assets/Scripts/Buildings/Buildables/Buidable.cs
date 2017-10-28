using UnityEngine;

/// <summary>
/// Entities that can be built by the player
/// </summary>
public abstract class Buildable : MonoBehaviour
{
    [Header("Buildable Settings")]
    [Tooltip("Building's description that appears in menus")]
    [SerializeField]
    protected string description;

    [SerializeField]
    protected int cost;

    protected Color originalColor;
    protected Renderer buildingRenderer;

    /// <summary>
    /// Colors green the buildable object
    /// </summary>
    public abstract void ColorGreen();

    /// <summary>
    /// Colors red the buildable object
    /// </summary>
    public abstract void ColorRed();

    /// <summary>
    /// Returns the buildable object color to its original
    /// </summary>
    public abstract void ColorOriginal();

    /// <summary>
    /// Executes when the building is placed
    /// </summary>
    public abstract void Place();

    public int Cost
    {
        get
        {
            return cost;
        }
    }

    /// <summary>
    /// Flag that indicates if the Info panel should be displayed when the mouse passes over.
    /// </summary>
    protected bool showInfoPanel = false;

    /// <summary>
    /// Reference to the UI panel tha diplays info about the building.
    /// </summary>
    protected DisplayGarbagePanel infoDisplay;


    /// <summary>
    /// Executed when the mouse enters the collider.
    /// Activates the Panel that displays the house's data.
    /// </summary>
    protected virtual void OnMouseEnter()
    {
        if(showInfoPanel && infoDisplay != null)
        {
            infoDisplay.DisplayPanel(description,
                displayOrdinary: false, ordinaryAmount: 0, ordinaryCapacityP: 0,
                displayGlass: false, glassAmount: 0, glassCapacityP: 0,
                displayMetal: false, metalAmount: 0, metalCapacityP: 0,
                displayPaper: false, paperAmount: 0, paperCapacityP: 0);
        }
    }

    /// <summary>
    /// Executed each frame the mouse is over the collider.
    /// Updates the info panel's data.
    /// </summary>
    protected virtual void OnMouseOver()
    {

        /*if (showInfoPanel && infoDisplay != null)
        {
            // Nothing for now
        }*/
    }

    /// <summary>
    /// Executed when the mouse leaves the collider.
    /// Hides the panel that displays the House's data.
    /// </summary>
    protected virtual void OnMouseExit()
    {
        if (showInfoPanel && infoDisplay != null)
            infoDisplay.gameObject.SetActive(false);
    }
}
