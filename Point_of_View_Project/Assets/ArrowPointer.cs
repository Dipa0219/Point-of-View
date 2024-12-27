using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public GameObject arrowPrefab;    // Prefab for the arrow UI element
    public Camera mainCamera;         // Reference to the main camera
    public RectTransform canvasRect;  // Reference to the UI Canvas RectTransform
    public Renderer targetRenderer;   // The Renderer for the player object

    private GameObject _arrowInstance;
    private bool _isArrowVisible = false;

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
            }
        }
    }

    void Update()
    {
        if( mainCamera.enabled == false)
        {
            _arrowInstance.SetActive(false);
            _isArrowVisible = false;
            return;
        }
        // Check visibility based on frustum and occlusion
        bool visibleInFrustum = IsVisibleFromCamera(targetRenderer, mainCamera);
        bool notOccluded = !IsOccluded();

        if (visibleInFrustum && notOccluded)
        {
            print("Player is visible");
            if (_isArrowVisible && _arrowInstance != null)
            {
                _arrowInstance.SetActive(false);
                _isArrowVisible = false;
            }
        }
        else
        {
            print("Player is not visible");
            if (!_isArrowVisible)
            {
                if (_arrowInstance == null)
                {
                    _arrowInstance = Instantiate(arrowPrefab, canvasRect);
                    
                }
                _arrowInstance.SetActive(true);
                _isArrowVisible = true;
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
        //Vector3 objectWorldPosition = targetRenderer.bounds.center;
        //Vector3 screenPoint = mainCamera.WorldToViewportPoint(objectWorldPosition);
        // Clamp the arrow within the screen boundaries
        //screenPoint.x = Mathf.Clamp(screenPoint.x, 10, Screen.width-10);
        //screenPoint.y = Mathf.Clamp(screenPoint.y, 10, Screen.height-10);
        print("pos" + transform.position.x);
        print("point: " + screenPoint.x);
        print("size: " + Screen.width);
        screenPoint.x = Mathf.Clamp(screenPoint.x, 0, Screen.width);// + Screen.width/2;
        screenPoint.y = Mathf.Clamp(screenPoint.y, 0, Screen.height);// + Screen.height/2;
        print("point_after: " + screenPoint.x);

        if (screenPoint.z < 0) // Handle case where object is behind the camera
        {
            screenPoint.z *= -1;
            screenPoint.y = 0;
            
            if (screenPoint.x > Screen.width / 2)
                screenPoint.x = screenPoint.x - 2 * ( screenPoint.x - (Screen.width / 2));
            else if (screenPoint.x < Screen.width / 2)
            {
                screenPoint.x = screenPoint.x + 2 * ( (Screen.width / 2)- screenPoint.x);
            }
            screenPoint.x -= Screen.width;
            //screenPoint.x = Mathf.Clamp(screenPoint.x, 10, Screen.width-10);
        }

        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Position the arrow on the canvas
        _arrowInstance.GetComponent<RectTransform>().localPosition = canvasPos;

        // Rotate the arrow to point toward the object
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _arrowInstance.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
    }
}
