using System;
using SaveManager;
using UnityEngine;
using UnityEngine.Serialization;

public class ArrowPointer : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;    // Prefab for the arrow UI element
    [SerializeField] private Camera otherBotCamera;         // Reference to the main camera
    [SerializeField] private RectTransform canvasRect;  // Reference to the UI Canvas RectTransform
    [SerializeField] private Renderer otherBotRenderer;   // The Renderer for the player object
    [SerializeField] private Transform target;           // The target object to point the arrow at

    [SerializeField] private Camera whiteBotCamera;
    [SerializeField] private Camera blackBotCamera;
    [SerializeField] private Transform whiteBot;
    [SerializeField] private Transform blackBot;
    
    private GameObject _arrowInstance;
    private bool _isArrowVisible = false;
    
    private bool _isEasyModeOn = true;

    void Start()
    {
        
        // check if in the settins the easy mode is on
        // if so update the boolean
        try
        {
            _isEasyModeOn = SaveSystem.checkEasyMode();
            _isEasyModeOn = true;
            print("set to true lo stesso");
        }
        catch (Exception ex)
        {
            print("easy mode catch exception");
            _isEasyModeOn = true;
        }
        
        if (_isEasyModeOn == false)
        {
            print("easy mode detected to false");
            return;
        }
        _arrowInstance = Instantiate(arrowPrefab, canvasRect);
        if (otherBotRenderer == null)
        {
            // Try to find the Renderer on this GameObject or its children
            otherBotRenderer = GetComponentInChildren<Renderer>();
            if (otherBotRenderer == null)
            {
                Debug.LogError("No Renderer found on the player or its children. Please assign one manually!");
                enabled = false; // Disable script if no Renderer is found
            }
        }
    }

    void Update()
    {
        if (_isEasyModeOn == false)
        {
            return;
        }
        
        if( otherBotCamera.enabled == false)
        {
            _arrowInstance.SetActive(false);
            _isArrowVisible = false;
            return;
        }
        // Check visibility based on frustum and occlusion
        bool visibleInFrustum = IsVisibleFromCamera(otherBotRenderer, otherBotCamera);
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
            if (!notOccluded)
            {
                print("occluded");
            }else
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
        if (gameObject.name == "White Bot")
        {
            print("white bot");
        }else
            print("black bot");
        
        
        //Vector3 direction = (transform.position - otherBotCamera.transform.position).normalized;
        Vector3 direction = (target.position - otherBotCamera.transform.position);//.normalized;
        float distance = Vector3.Distance(target.position, otherBotCamera.transform.position);

        // Perform a raycast to check for obstacles
        //if (Physics.Raycast(otherBotCamera.transform.position, direction, out RaycastHit hit, distance))
        LayerMask occlusionMask = LayerMask.GetMask("Default");

        if (Physics.Raycast(otherBotCamera.transform.position, direction, out RaycastHit hit, distance, occlusionMask))
        {
            // Check if the hit object is not the player
            return hit.transform != transform;
        }

        return false; // No obstacles found
        
        
        /*
        Vector3 direction = (blackBot.position - whiteBotCamera.transform.position);//.normalized;
        float distance = Vector3.Distance(blackBot.position, whiteBotCamera.transform.position);

        
        LayerMask occlusionMask = LayerMask.GetMask("Default");
        if (Physics.Raycast(whiteBotCamera.transform.position, direction, out RaycastHit hit, distance, occlusionMask))
        {
            if (gameObject.name == "White Bot")
                return hit.transform != blackBot;
            return hit.transform != whiteBot;
                
            
            
        }

        return false; // No obstacles found

        */
        
        //blackBot
    }

    void UpdateArrowPosition()
    {
        Vector3 screenPoint = otherBotCamera.WorldToScreenPoint(transform.position);
        //Vector3 objectWorldPosition = targetRenderer.bounds.center;
        //Vector3 screenPoint = mainCamera.WorldToViewportPoint(objectWorldPosition);
        // Clamp the arrow within the screen boundaries
        //screenPoint.x = Mathf.Clamp(screenPoint.x, 10, Screen.width-10);
        //screenPoint.y = Mathf.Clamp(screenPoint.y, 10, Screen.height-10);
        
        //print("pos" + transform.position.x);
        //print("point: " + screenPoint.x);
        //print("size: " + Screen.width);
        screenPoint.x = Mathf.Clamp(screenPoint.x, 0, Screen.width);// + Screen.width/2;
        screenPoint.y = Mathf.Clamp(screenPoint.y, 0, Screen.height);// + Screen.height/2;
        //print("point_after: " + screenPoint.x);

        if (screenPoint.z < 0) // Handle case where object is behind the camera
        {
            screenPoint.z *= -1;
            screenPoint.y = 0;
            
            if (screenPoint.x > Screen.width / 2)
                screenPoint.x = screenPoint.x - 2 * ( screenPoint.x - (Screen.width / 2));
            else if (screenPoint.x < Screen.width / 2)
            {
                screenPoint.x = screenPoint.x + 2 * ( (Screen.width / 2)- screenPoint.x);
            }/*
            screenPoint.x -= Screen.width;*/
            //screenPoint.x = Mathf.Clamp(screenPoint.x, 10, Screen.width-10);
            
            
        }
        
        //screenPoint.x = Mathf.Clamp(screenPoint.x, 15, Screen.width-15);// + Screen.width/2;
        //screenPoint.y = Mathf.Clamp(screenPoint.y, 15, Screen.height-15);// + Screen.height/2;
        if (screenPoint.x > Screen.width / 2)
            if (gameObject.name == "White Bot") screenPoint.x = screenPoint.x - 30;
                else screenPoint.x = screenPoint.x -90;
        else if (screenPoint.x < Screen.width / 2)
            if (gameObject.name == "White Bot") screenPoint.x = screenPoint.x + 30;
            else screenPoint.x = screenPoint.x + 90;

            
        if(screenPoint.y > Screen.height / 2)
            if (gameObject.name == "White Bot") screenPoint.y = screenPoint.y - 30;
        else if (screenPoint.y < Screen.height / 2)
            if (gameObject.name == "White Bot") screenPoint.y = screenPoint.y + 30;
            //else if (gameObject.name == "Black Bot") screenPoint.y = screenPoint.y - 30;
        
        
        screenPoint.x = Mathf.Clamp(screenPoint.x, 150, Screen.width-100);// + Screen.width/2;
        screenPoint.y = Mathf.Clamp(screenPoint.y, 70, Screen.height-25);
                
                
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Position the arrow on the canvas
        _arrowInstance.GetComponent<RectTransform>().localPosition = canvasPos;

        // Rotate the arrow to point toward the object
        Vector3 direction = (transform.position - otherBotCamera.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _arrowInstance.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
    }
}
