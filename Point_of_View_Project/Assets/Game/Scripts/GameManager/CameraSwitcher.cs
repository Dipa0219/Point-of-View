using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Movement cube1;
    [SerializeField] private Movement cube2;
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private Camera initialCamera;
    private bool _isActive = true;


    private void Start()
    {
        // Ensure only one camera is active at start (for example, camera1)
        camera1.enabled = true;
        camera1.GameObject().SetActive(true);
        camera2.enabled = false;
        camera2.GameObject().SetActive(false);
        cube1.SetActive(false);
        cube2.SetActive(true);
    }    
    
    /*private void Start()
    {
        // Ensure only one camera is active at start (for example, camera1)
        initialCamera.enabled = true;
        initialCamera.GameObject().SetActive(true);        
        camera1.enabled = false;
        camera1.GameObject().SetActive(false);
        camera2.enabled = false;
        camera2.GameObject().SetActive(false);
        cube1.SetActive(false);
        cube2.SetActive(false);
    }*/

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.ReloadLevel();
        }
        // Switch cameras when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C) && _isActive)
        {
            // Toggle the active camera
            if (camera1.enabled)
            {
                camera1.enabled = false;
                camera1.GameObject().SetActive(false);
                camera2.enabled = true;
                camera2.GameObject().SetActive(true);
                cube1.SetActive(true);
                cube2.SetActive(false);
            }
            else
            {
                camera1.enabled = true;
                camera1.GameObject().SetActive(true);
                camera2.enabled = false;
                camera2.GameObject().SetActive(false);
                cube1.SetActive(false);
                cube2.SetActive(true);
            }
        }
    }
    
    public void SetActive(bool active)
    {
        _isActive = active;
    }
}
