using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCondition : MonoBehaviour {

    public Camera mainCamera;
    private float lastUpdate;
    public GameObject panelGameOver;
    public GameObject panelDarkScreen;

    void Start ()
    {
        lastUpdate = 0f;
    }
	
	void Update () {
        //if (estado.CantidadDinero <=-50 && lastUpdate == 0)
        //{
        //    lastUpdate = Time.time;
        //}
        //else if (estado.CantidadDinero > -50 && lastUpdate != 0)
        //{
        //    lastUpdate = 0;
        //}
        if (Time.time >= lastUpdate + 10f && lastUpdate !=0)
        {
            panelDarkScreen.SetActive(true);
            panelGameOver.SetActive(true);
            Time.timeScale = 0;
        }
	}
}
