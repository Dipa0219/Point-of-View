using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : MonoBehaviour
{
    public GameObject arrowPrefab;    // Prefab for the arrow UI element
    public Camera mainCamera;         // Reference to the main camera
    public RectTransform canvasRect;  // Reference to the UI Canvas RectTransform
    public Renderer targetRenderer;   // The Renderer for the player object

    private GameObject arrowInstance;
    private bool isArrowVisible = false;

    void Start()
    {
        if (targetRenderer == null)
        {
            // Try to find the Renderer on this GameObject or its children
            targetRenderer = GetComponentInChildren<Renderer>();
            if (targetRenderer == null)
            {
                Debug.LogError("No Renderer found on the player or its children. Please assign one manually!");
                enabled = false; // Disable script if no Renderer is found
                return;
            }
        }
    }

    void Update()
    {
        // Check visibility based on frustum and occlusion
        bool visibleInFrustum = IsVisibleFromCamera(targetRenderer, mainCamera);
        bool notOccluded = !IsOccluded();

        if (visibleInFrustum && notOccluded)
        {
            print("Player is visible");
            if (isArrowVisible && arrowInstance != null)
            {
                arrowInstance.SetActive(false);
                isArrowVisible = false;
            }
        }
        else
        {
            print("Player is not visible");
            if (!isArrowVisible)
            {
                if (arrowInstance == null)
                {
                    arrowInstance = Instantiate(arrowPrefab, canvasRect);
                    
                }
                arrowInstance.SetActive(true);
                isArrowVisible = true;
            }
            //arrowInstance.GetComponent<CanvasRenderer>().SetAlpha(1f);
            UpdateArrowPosition();
        }
    }

    bool IsVisibleFromCamera(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    bool IsOccluded()
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        float distance = Vector3.Distance(transform.position, mainCamera.transform.position);

        // Perform a raycast to check for obstacles
        if (Physics.Raycast(mainCamera.transform.position, direction, out RaycastHit hit, distance))
        {
            // Check if the hit object is not the player
            return hit.transform != transform;
        }

        return false; // No obstacles found
    }

    void UpdateArrowPosition()
    {
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.position);

        // Clamp the arrow within the screen boundaries
        screenPoint.x = Mathf.Clamp(screenPoint.x, 10, Screen.width-10);
        screenPoint.y = Mathf.Clamp(screenPoint.y, 10, Screen.height-10);

        if (screenPoint.z < 0) // Handle case where object is behind the camera
        {
            screenPoint *= -1;
        }

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Position the arrow on the canvas
        arrowInstance.GetComponent<RectTransform>().localPosition = canvasPos;

        // Rotate the arrow to point toward the object
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowInstance.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
    }
}
