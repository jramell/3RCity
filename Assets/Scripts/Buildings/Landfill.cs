using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Landfill : TrashTreatmentCenter
{
    // --------------------------------------------------------
    // Attributes
    // --------------------------------------------------------

    /// <summary>
    /// Reference to the controller of the scene.
    /// </summary>
    private CityController controller;

    /// <summary>
    /// Reference to the UI panel tha diplays info about the landfill.
    /// </summary>
    private DisplayGarbagePanel infoDisplay;

    bool fillAlertDisplayed = false;


    // --------------------------------------------------------
    // Methods
    // --------------------------------------------------------
    protected override void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<CityController>();
        infoDisplay = controller.landfillInfoPanel;
        base.Start();
    }

    /// <summary>
    /// Executed when the mouse enters the collider.
    /// Activates the Panel that displays the house's data.
    /// </summary>
    private void OnMouseEnter()
    {
        infoDisplay.DisplayPanel(
            displayOrdinary: true, ordinaryAmount: trashDeposit.CurrentAmount, ordinaryCapacityP: maxCapacity,
            displayGlass: false, glassAmount: 0, glassCapacityP: 0,
            displayMetal: false, metalAmount: 0, metalCapacityP: 0,
            displayPaper: false, paperAmount: 0, paperCapacityP: 0);
    }

    /// <summary>
    /// Executed each frame the mouse is over the collider.
    /// Updates the info panel's data.
    /// </summary>
    private void OnMouseOver()
    {
        infoDisplay.UpdateValues(
            ordinaryAmount: trashDeposit.CurrentAmount,
            glassAmount: 0,
            metalAmount: 0,
            paperAmount: 0);
    }

    /// <summary>
    /// Executed when the mouse leaves the collider.
    /// Hides the panel that displays the House's data.
    /// </summary>
    private void OnMouseExit()
    {
        infoDisplay.gameObject.SetActive(false);
    }

    public override void ReceiveGarbage(int amountOfGarbage) {
        trashDeposit.DepositTrash(amountOfGarbage);
        float trashPercent = ((float)trashDeposit.CurrentAmount) / ((float)trashDeposit.Capacity);
        if (!fillAlertDisplayed &&  trashPercent >= 0.6f) {
            fillAlertDisplayed = true;
            Managers.EventManager.DisplayEventMessage(title: "Alerta vertedero", description: "¡Ministro, el vertedero está a punto de llenarse!"
                + "\n\nSi se llena por completo, toda la basura que intentemos meter se irá a las calles." 
                + "\n\nRecuerde que sólo va al vertedero la basura <color=#1ef20e>ordinaria</color>. Las cosas van mejor cuando usamos campañas de reciclaje.");
        }
    }

    public override void TreatGarbage() {
        //landfill doesn't treat garbage
    }
}
