using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines the behaviour of the UI panel that displays the Garbage of a selected building.
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class DisplayGarbagePanel : MonoBehaviour
{
    // ---------------------------------------------------------
    // Attributes
    // ---------------------------------------------------------

    public RectTransform titlePanel;
    public RectTransform extraInfoPanel;
    public RectTransform ordinaryGarbagePanel;
    public RectTransform paperPanel;
    public RectTransform glassPanel;
    public RectTransform metalPanel;

    public Text titleText;
    public Text extraInfoText;
    public Text ordinaryText;
    public Text paperText;
    public Text glassText;
    public Text metalText;    

    public Color normalColor = Color.white;
    public Color warningColor;
    public Color dangerColor;

    /// <summary>
    /// Reference to this object's RectTransform
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// The currently displayed house's maximun ordinary garbage capacity.
    /// </summary>
    private int ordinaryCapacity;

    /// <summary>
    /// The currently displayed house's maximun paper garbage capacity.
    /// </summary>
    private int paperCapacity;

    /// <summary>
    /// The currently displayed house's maximun glass garbage capacity.
    /// </summary>
    private int glassCapacity;

    /// <summary>
    /// The currently displayed house's maximun metal garbage capacity.
    /// </summary>
    private int metalCapacity;



    // ---------------------------------------------------------
    // Methods
    // ---------------------------------------------------------
    /// <summary>
    /// Displays the panel. It calculates and sets the Panel's Height based on the active child panels.
    /// </summary>
    /// <param name="displayOrdinary">Should display the ordinary garabge?</param>
    /// <param name="displayPaper">Should display the ordinary garabge?</param>
    /// <param name="displayMetal">Should display the ordinary garabge?</param>
    /// <param name="displayGlass">Should display the ordinary garabge?</param>
    /// <param name="ordinaryAmount"></param>
    /// <param name="paperAmount"></param>
    /// <param name="glassAmount"></param>
    /// <param name="metalAmount"></param>
    /// <param name="ordinaryCapacityP"></param>
    /// <param name="glassCapacityP"></param>
    /// <param name="paperCapacityP"></param>
    /// <param name="metalCapacityP"></param>
    public void DisplayPanel(bool displayOrdinary, bool displayPaper, bool displayMetal, bool displayGlass, int ordinaryAmount, int paperAmount, int glassAmount, int metalAmount, int ordinaryCapacityP, int glassCapacityP, int paperCapacityP, int metalCapacityP)
    {

        if (CityController.Current.Paused)
            return;

        rectTransform = GetComponent<RectTransform>();

        ordinaryCapacity = ordinaryCapacityP;
        paperCapacity = paperCapacityP;
        glassCapacity = glassCapacityP;
        metalCapacity = metalCapacityP;

        ordinaryGarbagePanel.gameObject.SetActive(displayOrdinary);
        paperPanel.gameObject.SetActive(displayPaper);
        glassPanel.gameObject.SetActive(displayGlass);
        metalPanel.gameObject.SetActive(displayMetal);
        gameObject.SetActive(true);

        // Get the height that the panel should have.
        float panelHeight = 7f;
        panelHeight += (titlePanel.gameObject.activeInHierarchy) ? titlePanel.sizeDelta.y : 0;
        panelHeight += (extraInfoPanel.gameObject.activeInHierarchy) ? extraInfoPanel.sizeDelta.y : 0;
        panelHeight += (ordinaryGarbagePanel.gameObject.activeInHierarchy) ? ordinaryGarbagePanel.sizeDelta.y : 0;
        panelHeight += (paperPanel.gameObject.activeInHierarchy) ? paperPanel.sizeDelta.y : 0;
        panelHeight += (glassPanel.gameObject.activeInHierarchy) ? glassPanel.sizeDelta.y : 0;
        panelHeight += (metalPanel.gameObject.activeInHierarchy) ? metalPanel.sizeDelta.y : 0;

        rectTransform.sizeDelta = new Vector2(0f, panelHeight); // this line resizes the panel.
        UpdateValues(ordinaryAmount, paperAmount, glassAmount, metalAmount);
        
    }

    /// <summary>
    /// Displays the panel. It calculates and sets the Panel's Height based on the active child panels.
    /// </summary>
    /// <param name="displayOrdinary">Should display the ordinary garabge?</param>
    /// <param name="displayPaper">Should display the ordinary garabge?</param>
    /// <param name="displayMetal">Should display the ordinary garabge?</param>
    /// <param name="displayGlass">Should display the ordinary garabge?</param>
    /// <param name="ordinaryAmount"></param>
    /// <param name="paperAmount"></param>
    /// <param name="glassAmount"></param>
    /// <param name="metalAmount"></param>
    /// <param name="ordinaryCapacityP"></param>
    /// <param name="glassCapacityP"></param>
    /// <param name="paperCapacityP"></param>
    /// <param name="metalCapacityP"></param>
    public void DisplayPanel(string extraInfo, bool displayOrdinary, bool displayPaper, bool displayMetal, bool displayGlass, int ordinaryAmount, int paperAmount, int glassAmount, int metalAmount, int ordinaryCapacityP, int glassCapacityP, int paperCapacityP, int metalCapacityP)
    {
        if (CityController.Current.Paused)
            return;

        rectTransform = GetComponent<RectTransform>();

        extraInfoText.text = extraInfo;
        ordinaryCapacity = ordinaryCapacityP;
        paperCapacity = paperCapacityP;
        glassCapacity = glassCapacityP;
        metalCapacity = metalCapacityP;

        ordinaryGarbagePanel.gameObject.SetActive(displayOrdinary);
        paperPanel.gameObject.SetActive(displayPaper);
        glassPanel.gameObject.SetActive(displayGlass);
        metalPanel.gameObject.SetActive(displayMetal);
        gameObject.SetActive(true);

        // Get the height that the panel should have.
        float panelHeight = 7f;
        panelHeight += (titlePanel.gameObject.activeInHierarchy) ? titlePanel.sizeDelta.y : 0;
        panelHeight += (extraInfoPanel.gameObject.activeInHierarchy) ? extraInfoPanel.sizeDelta.y : 0;
        panelHeight += (ordinaryGarbagePanel.gameObject.activeInHierarchy) ? ordinaryGarbagePanel.sizeDelta.y : 0;
        panelHeight += (paperPanel.gameObject.activeInHierarchy) ? paperPanel.sizeDelta.y : 0;
        panelHeight += (glassPanel.gameObject.activeInHierarchy) ? glassPanel.sizeDelta.y : 0;
        panelHeight += (metalPanel.gameObject.activeInHierarchy) ? metalPanel.sizeDelta.y : 0;

        rectTransform.sizeDelta = new Vector2(0f, panelHeight); // this line resizes the panel.
        UpdateValues(ordinaryAmount, paperAmount, glassAmount, metalAmount);

    }

    /// <summary>
    /// Displays the panel. It calculates and sets the Panel's Height based on the active child panels.
    /// </summary>  
    public void DisplayPanel(string title, string extraInfo, bool displayOrdinary, bool displayPaper, bool displayMetal, bool displayGlass, int ordinaryAmount, int paperAmount, int glassAmount, int metalAmount, int ordinaryCapacityP, int glassCapacityP, int paperCapacityP, int metalCapacityP)
    {
        if (CityController.Current.Paused)
            return;

        rectTransform = GetComponent<RectTransform>();

        titleText.text = title;
        extraInfoText.text = extraInfo;
        ordinaryCapacity = ordinaryCapacityP;
        paperCapacity = paperCapacityP;
        glassCapacity = glassCapacityP;
        metalCapacity = metalCapacityP;

        ordinaryGarbagePanel.gameObject.SetActive(displayOrdinary);
        paperPanel.gameObject.SetActive(displayPaper);
        glassPanel.gameObject.SetActive(displayGlass);
        metalPanel.gameObject.SetActive(displayMetal);
        gameObject.SetActive(true);

        // Get the height that the panel should have.
        float panelHeight = 7f;
        panelHeight += (titlePanel.gameObject.activeInHierarchy) ? titlePanel.sizeDelta.y : 0;
        panelHeight += (extraInfoPanel.gameObject.activeInHierarchy) ? extraInfoPanel.sizeDelta.y : 0;
        panelHeight += (ordinaryGarbagePanel.gameObject.activeInHierarchy) ? ordinaryGarbagePanel.sizeDelta.y : 0;
        panelHeight += (paperPanel.gameObject.activeInHierarchy) ? paperPanel.sizeDelta.y : 0;
        panelHeight += (glassPanel.gameObject.activeInHierarchy) ? glassPanel.sizeDelta.y : 0;
        panelHeight += (metalPanel.gameObject.activeInHierarchy) ? metalPanel.sizeDelta.y : 0;

        rectTransform.sizeDelta = new Vector2(0f, panelHeight); // this line resizes the panel.
        UpdateValues(ordinaryAmount, paperAmount, glassAmount, metalAmount);

    }

    /// <summary>
    /// Displays the panel. It calculates and sets the Panel's Height based on the active child panels.
    /// </summary>  
    public void DisplayPanel(string title, string extraInfo, bool displayExtraInfo, bool displayOrdinary, bool displayPaper, bool displayMetal, bool displayGlass, int ordinaryAmount, int paperAmount, int glassAmount, int metalAmount, int ordinaryCapacityP, int glassCapacityP, int paperCapacityP, int metalCapacityP)
    {
        if (CityController.Current.Paused)
            return;

        rectTransform = GetComponent<RectTransform>();

        titleText.text = title;
        extraInfoText.text = extraInfo;
        ordinaryCapacity = ordinaryCapacityP;
        paperCapacity = paperCapacityP;
        glassCapacity = glassCapacityP;
        metalCapacity = metalCapacityP;

        ordinaryGarbagePanel.gameObject.SetActive(displayOrdinary);
        paperPanel.gameObject.SetActive(displayPaper);
        glassPanel.gameObject.SetActive(displayGlass);
        metalPanel.gameObject.SetActive(displayMetal);
        extraInfoPanel.gameObject.SetActive(displayExtraInfo);
        gameObject.SetActive(true);

        // Get the height that the panel should have.
        float panelHeight = 7f;
        panelHeight += (titlePanel.gameObject.activeInHierarchy) ? titlePanel.sizeDelta.y : 0;
        panelHeight += (extraInfoPanel.gameObject.activeInHierarchy) ? extraInfoPanel.sizeDelta.y : 0;
        panelHeight += (ordinaryGarbagePanel.gameObject.activeInHierarchy) ? ordinaryGarbagePanel.sizeDelta.y : 0;
        panelHeight += (paperPanel.gameObject.activeInHierarchy) ? paperPanel.sizeDelta.y : 0;
        panelHeight += (glassPanel.gameObject.activeInHierarchy) ? glassPanel.sizeDelta.y : 0;
        panelHeight += (metalPanel.gameObject.activeInHierarchy) ? metalPanel.sizeDelta.y : 0;

        rectTransform.sizeDelta = new Vector2(0f, panelHeight); // this line resizes the panel.
        UpdateValues(ordinaryAmount, paperAmount, glassAmount, metalAmount);

    }

    /// <summary>
    /// Updates the values of the displayed data.
    /// </summary>
    /// <param name="ordinaryAmount"></param>
    /// <param name="paperAmount"></param>
    /// <param name="glassAmount"></param>
    /// <param name="metalAmount"></param>
    public void UpdateValues (int ordinaryAmount, int paperAmount, int glassAmount, int metalAmount)
    {
        if (CityController.Current.Paused)
            return;

        float percentage;

        percentage = (float)ordinaryAmount / (float)ordinaryCapacity;
        ordinaryText.text= ordinaryAmount.ToString() + "/" + ordinaryCapacity;
        if (percentage < 0.5f)
            ordinaryText.color = normalColor;
        else if (percentage >= 0.5f && percentage < 1)
            ordinaryText.color = warningColor;
        else
            ordinaryText.color = dangerColor;

        paperText.text = paperAmount.ToString() + "/" + paperCapacity;
        percentage = (float)paperAmount / (float)paperCapacity;
        if (percentage < 0.5f)
            paperText.color = normalColor;
        else if (percentage >= 0.5f && percentage < 1)
            paperText.color = warningColor;
        else
            paperText.color = dangerColor;

        glassText.text = glassAmount.ToString() + "/" + glassCapacity;
        percentage = (float)glassAmount / (float)glassCapacity;
        if (percentage < 0.5f)
            glassText.color = normalColor;
        else if (percentage >= 0.5f && percentage < 1)
            glassText.color = warningColor;
        else
            glassText.color = dangerColor;

        metalText.text = metalAmount.ToString() + "/" + metalCapacity;
        percentage = (float)metalAmount / (float)metalCapacity;
        if (percentage < 0.5f)
            metalText.color = normalColor;
        else if (percentage >= 0.5f && percentage < 1)
            metalText.color = warningColor;
        else
            metalText.color = dangerColor;

    }

}
