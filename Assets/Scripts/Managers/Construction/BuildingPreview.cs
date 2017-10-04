using UnityEngine;

public class BuildingPreview : MonoBehaviour {

    public LayerMask placeableLayerMask;

    [Tooltip("Layers where objects would prevent building from being placed")]
    public LayerMask obstacleLayerMask; 

    Buildings.Type previewingBuildingType;
    GameObject previewingBuilding;
    Buildable previewingBuildable;

    bool isPreviewPlaceable;

    void Update()
    {
        if (IsMouseInViewport())
        {
            PreviewBuilding();
            HandlePlayerClick();
        }
    }
    
    void PreviewBuilding()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, 200, placeableLayerMask)) 
        {
            previewingBuilding.transform.position = hit.point;
            isPreviewPlaceable = (!Physics.Raycast(mouseRay, 200, obstacleLayerMask)) && Managers.BuildingPlacementManager.
                            CanBuildingBePlacedInTile(previewingBuildingType, hit.collider.tag);
            if (isPreviewPlaceable)
            {
                previewingBuildable.ColorGreen();
                previewingBuilding.transform.position =
                    new Vector3(hit.collider.gameObject.transform.position.x + 1.8f,
                                hit.collider.gameObject.transform.position.y,
                                hit.collider.gameObject.transform.position.z + 2f); //should actually fix prefabs
                            //instead but that would mess up level generation
            }
            else
            {
                previewingBuildable.ColorRed();
            }
        }
    }
    
    /// <summary>
    /// Listens to player clicks and tries to place current building or cancels preview as appropriate.
    /// </summary>
    void HandlePlayerClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPreviewPlaceable)
            {
                Managers.BuildingPlacementManager.Place(previewingBuildable);
                StopPreview();
            }
            else
            {
                //give feedback that building can't be placed, like playing a sound
            }
        } 
        if (Input.GetMouseButtonDown(1))
        {
            CancelPreview();
        }
    }

	public void StartBuildingPreview(Buildings.Type buildingType)
    {
        previewingBuildingType = buildingType;
        previewingBuilding = Managers.PrefabManager.MapBuildingToPrefab(buildingType);
        previewingBuilding = Instantiate(previewingBuilding, new Vector3(-100f, -100f, -100f), Quaternion.identity);
        previewingBuildable = previewingBuilding.GetComponent<Buildable>();
        enabled = true;
    }

    public void StopPreview()
    {
        previewingBuilding = null;
        enabled = false;
    }

    bool IsMouseInViewport()
    {
        return Input.mousePosition.x >= 0f && Input.mousePosition.y >= 0f
            && Input.mousePosition.x <= Screen.width && Input.mousePosition.y <= Screen.height;
    }

    public void CancelPreview()
    {
        if (previewingBuilding != null) {
            Destroy(previewingBuilding);
            previewingBuilding = null;
        }
        StopPreview();
    }
}
