using UnityEngine;

public class GazeDetection : MonoBehaviour
{
    [Header("Settings")]
    public float gazeDistance = 5f;
    public LayerMask watchLayer;
    public GameObject radialMenu;

    [Header("Optional Debugging")]
    public bool showDebugRay = true;

    private GameObject currentGazedObject;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Vector3 origin = mainCam.transform.position;
        Vector3 direction = mainCam.transform.forward;

        if (showDebugRay)
            Debug.DrawRay(origin, direction * gazeDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, gazeDistance, watchLayer))
        {
            if (hit.collider.CompareTag("Watch"))
            {
                if (currentGazedObject != hit.collider.gameObject)
                {
                    currentGazedObject = hit.collider.gameObject;
                    ShowRadialMenu();
                }
            }
            else
            {
                ClearGaze();
            }
        }
        else
        {
            ClearGaze();
        }
    }

    void ShowRadialMenu()
    {
        if (radialMenu != null && !radialMenu.activeSelf)
        {
            radialMenu.SetActive(true);
        }
    }

    void ClearGaze()
    {
        if (currentGazedObject != null)
        {
            currentGazedObject = null;
            if (radialMenu != null)
            {
                radialMenu.SetActive(false);
            }
        }
    }
}
