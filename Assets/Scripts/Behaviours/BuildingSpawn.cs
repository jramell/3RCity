using UnityEngine;
using UnityEngine.UI;

public class BuildingSpawn : MonoBehaviour {

    public Button botonPremios;
    public Button botonPublicidad;
    public Transform edificioPublicidad;
    public Transform fabricaOrdinaria;
    public Transform edif2;
    public Transform edifPapel;
    public Transform edifPlastico;
    public Transform edifVidrio;
    public GameObject camionBase;
    public Transform camionPapel;
    public Transform camionPlastico;
    public Transform camionVidrio;
    public AudioClip contructionAudio;

    private bool initCamiones;
    RaycastHit vHit;
    Ray vRay;
    Vector3 clickedObjectPosition;
    private UIInit uiMessage;
    public static int OrdInit;
    private int PapelInit;
    private int PlasticoInit;
    private int VidrioInit;
    public static int publicidad;
}