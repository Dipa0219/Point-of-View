using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        // Ensure only one camera is active at start (for example, camera1)
        camera1.enabled = true;
        camera1.GameObject().SetActive(true);
        camera2.enabled = false;
        camera2.GameObject().SetActive(false);
    }

    void Update()
    {
        // Switch cameras when the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle the active camera
            if (camera1.enabled)
            {
                camera1.enabled = false;
                camera1.GameObject().SetActive(false);
                camera2.enabled = true;
                camera2.GameObject().SetActive(true);
            }
            else
            {
                camera1.enabled = true;
                camera1.GameObject().SetActive(true);
                camera2.enabled = false;
                camera2.GameObject().SetActive(false);
            }
        }
    }
}
