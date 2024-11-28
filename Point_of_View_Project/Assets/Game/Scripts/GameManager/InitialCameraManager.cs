using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GameManager



{ 
    
    
    
    
    
    
    
    public class CameraWaypoint : MonoBehaviour
    {
        [SerializeField] private Transform[] waypoints;  // Array of waypoints
        public float rotateSpeed = 1f; // Speed for rotation
        public float rotationAngle = 60f; // Rotation angle at waypoint
        private int currentWaypoint = 0;
    
        private bool isRotating = false;
        private bool hasStopped = false;
        
        [SerializeField] private GameObject[] playerLight;
        [SerializeField] private GameObject[] ambientLight;
        
        [SerializeField] private CameraSwitcher cameraSwitcher;

    
        void Update()
        {
            if (!isRotating && !hasStopped && waypoints.Length > 0)
            {
                GoToNextWaypoint();
            }
        }
    
        void GoToNextWaypoint()
        {
            if (currentWaypoint >= waypoints.Length)
            {
                hasStopped = true;
                StartLevel(); // Stop when the last waypoint is reached
                return;
            }
    
            // Directly translate the camera to the next waypoint
            Transform target = waypoints[currentWaypoint];
            Vector3 direction = target.position - transform.position;
    
            // Translate instantly to the position of the next waypoint
            transform.Translate(direction, Space.World);
    
            // Start rotating at the waypoint
            isRotating = true;
            StartCoroutine(RotateAtWaypoint());
        }
    
        
        System.Collections.IEnumerator RotateAtWaypoint()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, rotationAngle, 0));
            float time = 0;
    
            while (time < 1)
            {
                time += Time.deltaTime * rotateSpeed;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
            }
    
            isRotating = false;
    
            // Move to the next waypoint if not at the last
            currentWaypoint++;
        }
        
        
        
        void StartLevel(){
            cameraSwitcher.SwitchCameras();
            for (int j = 0; j < playerLight.Length; j++)
            {
                playerLight[j].SetActive(true);
            }
            for (int j = 0; j < ambientLight.Length; j++)
            {
                ambientLight[j].SetActive(false);
            }    
        }
    }
    
    
    
    
    
    


}

