using UnityEngine;

public enum ControlType { PC, Mobile}
public class BuildManager : MonoBehaviour
{
    public GameObject PrefabBuild;
    public GameObject CreatedBuild;

    public bool IsBuilding;
    public bool IsButton;

    [SerializeField] private ControlType controlType;

    private Vector3 placePosition;
    private Vector3 placeRotation;
    private Quaternion currentRotation;

    void LateUpdate()
    {   
        if (CreatedBuild != null)
        {
            currentRotation = Quaternion.Lerp(CreatedBuild.transform.rotation, Quaternion.Euler(placeRotation), 0.2f);
            CreatedBuild.transform.rotation = currentRotation;
        }
        
        if(controlType == ControlType.PC && IsBuilding)
            SetPlacementPC();
        else
            SetPlacement();
    }

    private void SetPlacement()
    {
        RaycastHit hit;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
            {
                if (IsButton)
                    return;

                placePosition = new Vector3(Mathf.Round(hit.point.x / 3f) * 3f, hit.point.y + 1.5f, Mathf.Round(hit.point.z / 3f) * 3f); 
            }
        }

        if (PrefabBuild != null)
        {
            if (CreatedBuild == null)
            {
                CreatedBuild = Instantiate(PrefabBuild, placePosition, Quaternion.Euler(placeRotation));
            }
            else
            {
                CreatedBuild.transform.position = Vector3.Lerp(CreatedBuild.transform.position, placePosition, 10f * Time.deltaTime);
            }
        }
    }

    private void SetPlacementPC()
    {
        RaycastHit hit;

        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
        {
            if (IsButton)
                return;

            placePosition = new Vector3(Mathf.Round(hit.point.x / 3f) * 3f, hit.point.y + 1.5f, Mathf.Round(hit.point.z / 3f) * 3f);

            if (PrefabBuild != null)
            {
                if (CreatedBuild == null)
                {
                    CreatedBuild = Instantiate(PrefabBuild, placePosition, Quaternion.Euler(placeRotation));
                }
                else
                {
                    CreatedBuild.transform.position = Vector3.Lerp(CreatedBuild.transform.position, placePosition, 10f * Time.deltaTime);
                }
            }

            Debug.DrawRay(ray.origin, hit.point);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ApplyPlacement();
        }
    }

    public void RotatePlacement()
    {
        placeRotation += new Vector3(0, 90f, 0);
    }

    public void ApplyPlacement()
    {
        if (CreatedBuild.GetComponent<PrefabBuild>().Place(placePosition, placeRotation))
        {
            CreatedBuild = null;
            return;
        }
    }

    public void CancelPlacement()
    {
        IsBuilding = false;
        Destroy(CreatedBuild.gameObject);
        PrefabBuild = null;
    }
}
