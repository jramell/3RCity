using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRecyclingCenter : TrashTreatmentCenter {

    [Range(0, 10)]
    public float revenuePerPiece = 0.2f;

    void Start() {
        trashDeposit = new TrashCan(Garbage.Type.Glass, maxCapacity);
    }

    public override void TreatGarbage()
    {
        float revenue = trashDeposit.CurrentAmount * revenuePerPiece;
        //Debug.Log((revenue > 0) ? "glass: " + revenue : "");
        trashDeposit.CurrentAmount = 0;       
        CityController.Current.CurrentMoney += (int)revenue;
    }

}
