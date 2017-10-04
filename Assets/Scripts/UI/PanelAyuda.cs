using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAyuda : MonoBehaviour {

    //tooltip text
    public Text thisText;

    //tooltip background image
    public RectTransform bgImage;
    Image bgImageSource;

    //needed as the layout refreshes only on the first Update() call
    bool firstUpdate;

    void Start()
    {
        //in this line you need to change the string in order to get your Camera //TODO MAYBE DO IT FROM THE INSPECTOR
        bgImageSource = bgImage.GetComponent<Image>();

        //hide the tooltip
        HideTooltipVisibility();
        this.transform.gameObject.SetActive(false);
    }

    //single string input tooltip
    public void SetTooltip(string text)
    {
        //init tooltip string
        thisText.text = text;
        ActivateTooltipVisibility();
    }

    //multi string/line input tooltip (each string of the input array is a new line)
    public void SetTooltip(string[] texts)
    {
        //build up the tooltip line after line with the input
        string tooltipText = "";
        int index = 0;
        foreach (string newLine in texts)
        {
            if (index == 0)
            {
                tooltipText += newLine;
            }
            else {
                tooltipText += ("\n" + newLine);
            }
            index++;
        }
        thisText.text = tooltipText;
        ActivateTooltipVisibility();
    }

    //call to hide tooltip when hovering out from the object
    public void HideTooltip()
    {
        if (this != null)
        {
            this.transform.gameObject.SetActive(false);
            HideTooltipVisibility();
        }
    }

    void Update()
    {
            this.transform.gameObject.transform.position = Input.mousePosition; 
    }

    //this function is used in order to setup the size of the tooltip by cheating on the HorizontalLayoutBehavior. The resize is done in the first update.

    //used to visualize the tooltip one update call after it has been built (to avoid flickers)
    public void ActivateTooltipVisibility()
    {
        this.transform.gameObject.SetActive(true);
        Color textColor = thisText.color;
        thisText.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
        bgImageSource.color = new Color(bgImageSource.color.r, bgImageSource.color.g, bgImageSource.color.b, 0.8f);
    }

    //used to hide the tooltip so that it can be made visible one update call after it has been built (to avoid flickers)
    public void HideTooltipVisibility()
    {
        Color textColor = thisText.color;
        thisText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
        bgImageSource.color = new Color(bgImageSource.color.r, bgImageSource.color.g, bgImageSource.color.b, 0f);
    }
}

