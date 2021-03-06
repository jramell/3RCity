﻿using UnityEngine;

/// <summary>
/// Moves object it's attached to along its local X and Z axes according to mouse position within viewport. Made
/// to handle camera panning. Doesn't move any camera directly.
/// </summary>
public class CameraPanningController : MonoBehaviour
{
    [Header("Movement Settings")]
    /// <summary>
    /// This object will move if mouse is closer than this percent of the screen width to the left/right
    /// </summary>
    [Tooltip("Camera will move if mouse is closer than this percent of the screen width to the left/right")]
    [Range(0.0f, 1.0f)]
    public float WidthPercentageSideThreshold = 0.1f;

    /// <summary>
    /// This object will move if mouse is closer than this percent of screen height to the top/bottom
    /// </summary>
    [Tooltip("Camera will move if mouse is closer than this percent of screen height to the top/bottom")]
    [Range(0.0f, 1.0f)]
    public float HeightPercentageTopThreshold = 0.1f;

    /// <summary>
    /// When screen is too small, this object will move if mouse is closer than this to the left/right
    /// </summary>
    [Tooltip("When screen is too small, camera will move if mouse is closer than this to the left/right")]
    public float MinSideThreshold = 15f;

    /// <summary>
    /// When screen is too small, this object will move if mouse is closer than this to the top/bottom
    /// </summary>
    [Tooltip("When screen is too small, camera will move if mouse is closer than this to the top/bottom")]
    public float MinTopThreshold = 15f;

    public float MovementSpeed = 20f;

    [Header("Limit Settings")]
    /// <summary>
    /// This object won't move in its local +X more than this
    /// </summary>
    [Range(0.0f, 100000f)]
    [Tooltip("Camera won't move more to its right (+X axis) than this")]
    public float MaxRightTranslation;

    /// <summary>
    /// This object won't move won't move in its local -X more than this
    /// </summary>
    [Range(-100000f, 0.0f)]
    [Tooltip("Camera won't move more to its left (-X axis) than this")]
    public float MaxLeftTranslation;

    /// <summary>
    /// This object won't move in its local +Z more than this
    /// </summary>
    [Range(0.0f, 100000f)]
    [Tooltip("Camera won't move forward (its +Z axis) more than this")]
    public float MaxForwardTranslation;

    /// <summary>
    /// This object won't move in its local -Z more than this
    /// </summary>
    [Range(-100000f, 0.0f)]
    [Tooltip("Camera won't move backwards (its -Z axis) more than this")]
    public float MaxBackwardTranslation;

    /// <summary>
    /// Translation along local X axis from this object's starting position (its local 0).
    /// </summary>
    private float XTranslationFromStartingPosition = 0f;

    /// <summary>
    /// Translation along local Z axis from this object's starting position (its local 0).
    /// </summary>
    private float ZTranslationFromStartingPosition = 0f;

    /// <summary>
    /// Amount the camera is going to be translated in the current frame
    /// </summary>
    private Vector3 translateAmount;

    /// <summary>
    /// If mouse is closer to left/right of the screen than this, horizontal panning will occur
    /// </summary>
    private float currentSideThreshold;

    /// <summary>
    /// If mouse is closer to top/bottom of the screen than this, vertical panning will occur
    /// </summary>
    private float currentTopThreshold;

    /// <summary>
    /// Is 0, 1 or -1. 1 means positive on the X axis, -1 means negative.
    /// </summary>
    private int directionX = 0;

    /// <summary>
    /// Is 0, 1 or -1. 1 means positive on the Z axis, -1 means negative.
    /// </summary>
    private int directionZ = 0;

    private void Start() {
        translateAmount = Vector3.zero;
        float percentualSideThreshold = WidthPercentageSideThreshold * Screen.width;
        currentSideThreshold = Mathf.Max(MinSideThreshold, percentualSideThreshold);

        float percentualTopThreshold = HeightPercentageTopThreshold * Screen.height;
        currentTopThreshold = Mathf.Max(MinTopThreshold, percentualTopThreshold);
    }

    private void Update() {
        directionX = 0;
        directionZ = 0;
        ManageCameraPanning();
    }

    void ManageCameraPanning() {
        FindPanningDirection();
        translateAmount.x = MovementSpeed * Time.deltaTime * directionX;
        translateAmount.z = MovementSpeed * Time.deltaTime * directionZ;
        KeepTranslationInLimits();
        transform.Translate(translateAmount);
        XTranslationFromStartingPosition += translateAmount.x;
        ZTranslationFromStartingPosition += translateAmount.z;
    }

    /// <summary>
    /// Checks mouse position within viewport to determine the direction in which the player
    /// wants to pan the camera.
    /// </summary>
    private void FindPanningDirection() {
        if (Input.mousePosition.x < currentSideThreshold) {
            directionX = -1;
        } else if (Input.mousePosition.x > Screen.width - currentSideThreshold) {
            directionX = 1;
        } if (Input.mousePosition.y < currentTopThreshold) {
            directionZ = -1;
        } else if (Input.mousePosition.y > Screen.height - currentTopThreshold) {
            directionZ = 1;
        }
    }

    /// <summary>
    /// Doesn't allow the camera to move beyond MaxRightTranslation, MaxLeftTranslation, MaxBackwardTranslation
    /// and MaxForwardTranslation
    /// </summary>
    private void KeepTranslationInLimits() {
        KeepXTranslationInLimits();
        KeepZTranslationInLimits();
    }

    private void KeepXTranslationInLimits() {
        bool goingBeyondMaxLeftTranslation = XTranslationFromStartingPosition + translateAmount.x < MaxLeftTranslation;
        bool goingBeyondMaxRightTranslation = XTranslationFromStartingPosition + translateAmount.x > MaxRightTranslation;
        if (goingBeyondMaxLeftTranslation) {
            float distanceFromLeftLimit = MaxLeftTranslation - XTranslationFromStartingPosition;
            translateAmount.x = distanceFromLeftLimit; //move up to MaxLeftTranslation
        } else if (goingBeyondMaxRightTranslation) {
            float distanceFromRightLimit = MaxRightTranslation - XTranslationFromStartingPosition;
            translateAmount.x = distanceFromRightLimit; //move up to MaxRightTranslation
        }
    }

    private void KeepZTranslationInLimits() {
        bool goingBeyondMaxBackwardTranslation = ZTranslationFromStartingPosition + translateAmount.z < MaxBackwardTranslation;
        bool goingBeyondMaxForwardTranslation = ZTranslationFromStartingPosition + translateAmount.z > MaxForwardTranslation;
        if (goingBeyondMaxBackwardTranslation) {
            translateAmount.z = MaxBackwardTranslation - ZTranslationFromStartingPosition;
        } else if (goingBeyondMaxForwardTranslation) {
            translateAmount.z = MaxForwardTranslation - ZTranslationFromStartingPosition;
        }
    }
}