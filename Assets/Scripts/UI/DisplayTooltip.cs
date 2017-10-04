using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject tooltip;

    private bool followMouse;

    void Start() {
        followMouse = tooltip.GetComponent<FollowMouse>() != null;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (followMouse) {
            tooltip.transform.position = Input.mousePosition;
        }
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.SetActive(false);
    }
}
