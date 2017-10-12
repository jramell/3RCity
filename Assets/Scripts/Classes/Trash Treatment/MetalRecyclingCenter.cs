using System;
using UnityEngine;

public class MetalRecyclingCenter : TrashTreatmentCenter {

    [Range(0,10)]
    public float revenuePerPiece = 0.3f;

    void Start() {
        trashDeposit = new TrashCan(Garbage.Type.Metal, maxCapacity);
    }

    public override void TreatGarbage()
    {
        //do something with this, sometime. It is not called automatically by the TrashTreatmentCenter.
        //you can call it every X amount of time, or override the ReceiveGarbage(int amountOfGarbage) method
        //to call this within it
        int revenue = Convert.ToInt32(trashDeposit.CurrentAmount * revenuePerPiece);
        if (revenue > 0) {
            trashDeposit.CurrentAmount = 0;
            CityController.Current.CurrentMoney += revenue;
        }
    }
}

