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

    public override void TreatGarbage() {
        //landfill doesn't treat garbage
    }
}
