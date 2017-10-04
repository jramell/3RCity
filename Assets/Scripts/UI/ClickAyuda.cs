using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAyuda : MonoBehaviour {

    private UIInit uiInit;

    void Start()
    {
        uiInit = FindObjectOfType<UIInit>();
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //if (this.gameObject.tag.Equals("PisoTipo1"))
            //{
            //    estadoJuego.MensajeInformacion = "Aquí se puede construir edificios de tamaño 1";
            //}
            //else if (this.gameObject.tag.Equals("PisoTipo2"))
            //{
            //    estadoJuego.MensajeInformacion = "Aquí se puede construir edificios de tamaño 2";
            //}
            //else if (this.gameObject.tag.Equals("PisoTipo3"))
            //{
            //    estadoJuego.MensajeInformacion = "Aquí se puede construir edificios de tamaño 3";
            //}
            
            //if (!estadoJuego.HayMensaje)
            //{
            //    StartCoroutine(uiInit.mostrarYBorrar());
            //}
        }
    }
}
