using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarEstadisticasBoton : MonoBehaviour {
    private Button esteBoton;
    public GameObject panelEstadisticas;
    public GameObject panelBalance;
    void Start()
    {
        esteBoton = this.GetComponent<Button>();
        esteBoton.onClick.AddListener(mostrarEsconderEstadisticas);
    }
	// Use this for initialization
    void mostrarEsconderEstadisticas()
    {
        if (panelEstadisticas.activeInHierarchy == false && panelBalance.activeInHierarchy == false)
        {
            panelEstadisticas.SetActive(true);
            panelBalance.SetActive(true);
        }
        else
        {
            panelEstadisticas.SetActive(false);
            panelBalance.SetActive(false);
        }
    }
}
