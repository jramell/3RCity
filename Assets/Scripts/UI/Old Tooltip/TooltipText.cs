using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TooltipText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tooltipText;
    private PanelAyuda panel;
    public HorizontalLayoutGroup recuadroPorPivot;

    void Start()
    {
        panel = recuadroPorPivot.GetComponent<PanelAyuda>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetTooltip(tooltipText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.HideTooltip();
    }

}
