using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBehaviour : MonoBehaviour {

    private float lastBalance;

	void Update () {
        if (Time.time - lastBalance >= 3.25f)
        {
            lastBalance = Time.time;
        }
	}
}
