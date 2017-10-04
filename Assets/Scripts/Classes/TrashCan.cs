using UnityEngine;

/// <summary>
/// Manages trash in the streets logic related to trash deposit. If trash is deposited when over 
/// capacity, trash in streets will increase. If trash is withrawed when the can's trash is in the 
/// streets, trash in the streets will decrease.
/// </summary>
public class TrashCan {

    Garbage.Type type;
    int capacity;
    int currentAmount;
    int amountInStreets;

    public Garbage.Type Type {
        get { return type; }
    }

    public int Capacity {
        get { return capacity; }
        set { capacity = value; }
    }

    public int CurrentAmount {
        get { return currentAmount; }
        set {
            currentAmount = value;
        }
    }

    public TrashCan(Garbage.Type type, int capacity) {
        this.type = type;
        this.capacity = capacity;
    }

    /// <summary>
    /// Withraws trash from the trash can. If there was trash in the streets when picking up,
    /// reduces trash in the streets
    /// </summary>
    /// <param name="pickupAmount">Amount of trash to withraw</param>
    public void PickupTrash(int pickupAmount) {
        if (amountInStreets > 0) {
            CityController.Current.TrashInStreets -= Mathf.Min(pickupAmount, amountInStreets);
            amountInStreets -= Mathf.Min(pickupAmount, amountInStreets);
        }
        currentAmount -= pickupAmount;
    }

    /// <summary>
    /// Deposits trash in the trash can. If the trash can is over its max capacity, increases
    /// trash in the streets
    /// </summary>
    /// <param name="depositAmount">Amount of trash to deposit</param>
    public void DepositTrash(int depositAmount) {
        if (currentAmount > capacity) {
            amountInStreets += depositAmount;
            CityController.Current.TrashInStreets += depositAmount;
        }
        else if (currentAmount + depositAmount > capacity) {
            amountInStreets += (currentAmount + depositAmount) - capacity;
            CityController.Current.TrashInStreets += amountInStreets;
        }
        currentAmount += depositAmount;
    }
}