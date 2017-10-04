using UnityEngine;

public class MuerteArboles : MonoBehaviour {

    private float lastUpdate;

    void Start ()
    {
        lastUpdate = 0f;
	}
	
	void Update ()
    {
    //    if (estado.CantidadDinero <= -50 && lastUpdate == 0)
    //    {
    //        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
    //        lastUpdate = Time.time;
    //    }
    //    else if (Time.time >= lastUpdate + 1f && Time.time < lastUpdate + 2f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 2f && Time.time < lastUpdate + 3f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 3f && Time.time < lastUpdate + 4f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(4).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 4f && Time.time < lastUpdate + 5f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 5f && Time.time < lastUpdate + 6f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(6).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 6f && Time.time < lastUpdate + 7f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //    }
    //    else if (Time.time >= lastUpdate + 7f && estado.CantidadDinero <= -50)
    //    {
    //        this.gameObject.transform.GetChild(7).gameObject.SetActive(false);
    //    }
    //    //Cuando se empieza a recuperar
    //    else if (estado.CantidadDinero > -50 && lastUpdate != 0)
    //    {
    //        lastUpdate = Time.time;
    //        this.gameObject.transform.GetChild(7).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 1f && Time.time < lastUpdate + 2f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 2f && Time.time < lastUpdate + 3f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 3f && Time.time < lastUpdate + 4f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 4f && Time.time < lastUpdate + 5f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 5f && Time.time < lastUpdate + 6f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 6f && Time.time < lastUpdate + 7f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    //    }
    //    else if (Time.time >= lastUpdate + 7f && estado.CantidadDinero > -50)
    //    {
    //        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //        lastUpdate = 0;
    //    }
    }
}
