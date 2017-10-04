using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TooltipTextNotActivated : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public string tooltipText;
    public string linea1;
    public string linea2;
    public string linea3;
    private PanelAyuda panel;
    public HorizontalLayoutGroup recuadroPorPivot;
    public Button esteBoton;
    public bool usarMultiplesLineas;

    void Start()
    {
        panel = recuadroPorPivot.GetComponent<PanelAyuda>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (esteBoton.interactable == false)
        {
            if (usarMultiplesLineas==true)
            {
                panel.SetTooltip(new string[] { linea1, linea2, linea3 });
            }
            else
            {
                panel.SetTooltip(tooltipText);
            }
            
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (esteBoton.interactable == false)
        {
            panel.HideTooltip();
        }
    }
}
