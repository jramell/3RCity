using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPublicidad : MonoBehaviour {

    public Button btnPublicidad0;
    public Button btnPublicidad1;
    public Button btnPublicidad2;
    public Button btnPublicidad3;
    public Button btnPublicidad4;
    public Text textoDescr;

    // Use this for initialization
    void Awake () {

        Button btn1 = btnPublicidad0.GetComponent<Button>();
        Button btn2 = btnPublicidad1.GetComponent<Button>();
        btn1.onClick.AddListener(aplicarPublicidadPuertaAPuerta);
        btn2.onClick.AddListener(aplicarPublicidad2);
        Button btn3 = btnPublicidad2.GetComponent<Button>();
        btn3.onClick.AddListener(aplicarPublicidad3);

        Button btn4 = btnPublicidad3.GetComponent<Button>();
        btn4.onClick.AddListener(aplicarPublicidad4);

        Button btn5 = btnPublicidad4.GetComponent<Button>();
        btn5.onClick.AddListener(aplicarPublicidad5);
    }

    void aplicarPublicidadPuertaAPuerta()
    {
        //AplicarPublicidad.numMensaje = 1;
        textoDescr.text = "Una campaña informativa “puerta a puerta” sobre separación de basura se implementará en la ciudad."
           + " Es reciclable el plástico (Caneca azul), el vidrio (Caneca vidrio), el papel y cartón (Caneca gris), "
           + " siempre que se encuentren limpios y secos. \n"
           + " Costo: 0$ \n"
           + " Beneficio: Ahora los ciudadanos saben reciclar un poco y tienen canecas separadas";
    }

    void aplicarPublicidad2()
    {
        //AplicarPublicidad.numMensaje = 2;
        textoDescr.text = "Los periodicos locales han reconocido el esfuerzo del ministro de ambiente y han decidido a ayudar"
            + " con la campaña de reciclaje mediante columnas dedicadas a enseñar buenas practicas de reciclaje.\n"
            + " Costo: 50$";
    }

    void aplicarPublicidad3()
    {
        //AplicarPublicidad.numMensaje = 3;
        textoDescr.text = "Algunas emisoras de radio han accedido a emitir información relacionada con el reciclaje"
            + " y esperamos que los oyentes aprendan algo en donde quiera que se encuentren.\n"
            + " Costo: 50$";
    }

    void aplicarPublicidad4()
    {
        //AplicarPublicidad.numMensaje = 4;
        textoDescr.text = "El parque ha sido de gran ayuda para reunir grandes cantidades de personas"
            + " y eso te permite realizar una gran campaña informativa para ayudar a los cuidadanos con dudas"
            + " a entender mejor el proceso de reciclaje que se lleva a cabo desde las casas. \n"
            + " Costo: 150$";
    }

    void aplicarPublicidad5()
    {
        //AplicarPublicidad.numMensaje = 5;
        textoDescr.text = "El museo permite a los cuidadanos ver la historia de la ciudad y de cuanto ha cambiado a lo largo"
            + " de los tiempos, más aún cuando se incorporo el reciclaje. La gente que viene siente que esta ayudando al ambiente de la ciudad"
            + " y desean continuar mejorando sus habilidades de reciclaje. \n"
            + " Costo: 200$";
    }
}
