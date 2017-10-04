using UnityEngine;

public abstract class TrashTreatmentCenter : MonoBehaviour
{
    // --------------------------------------------------------
    // Attributes and properties
    // --------------------------------------------------------

    [Header("Trash Treatment Center Settings")]
    
    [SerializeField]
    [Tooltip("Where trucks will go to deposit their trash in the center")]
    protected Transform truckStop;

    [SerializeField]
    [Tooltip("Amount of trash the center can hold before overflowing")]
    protected int maxCapacity;

    protected TrashCan trashDeposit;

    public Transform TruckStop
    {
        get { return truckStop; }
    }

    public TrashCan TrashDeposit {
        get { return trashDeposit; }
    }

    // --------------------------------------------------------
    // Methods
    // --------------------------------------------------------

    protected virtual void Start()
    {
        trashDeposit = new TrashCan(Garbage.Type.Ordinary, maxCapacity);
    }

    /// <summary>
    /// Do something with the current Garbage.
    /// </summary>
    public abstract void TreatGarbage();

    /// <summary>
    /// Deposits garbage in depositedGarbage into this center
    /// </summary>
    /// <param name="amountOfGarbage">Amount of garbage to deposit</param>
    public virtual void ReceiveGarbage(int amountOfGarbage)
    {
        trashDeposit.DepositTrash(amountOfGarbage);
    }
}
