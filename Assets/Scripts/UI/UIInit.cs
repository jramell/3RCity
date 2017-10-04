using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInit : MonoBehaviour {
    public Transform canvas;
    public Transform panelInformacion;
    private Text textoInformativo;
    public Transform panelPublicidad;

    void Start ()
    {
        textoInformativo = panelInformacion.GetChild(0).GetComponent<Text>();
        panelInformacion.gameObject.SetActive(false);
    }
}
