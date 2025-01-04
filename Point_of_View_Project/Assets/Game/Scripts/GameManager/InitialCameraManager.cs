using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GameManager

{
    
    public class CameraWaypoint : MonoBehaviour
    {
        [System.Serializable] struct Waypoints
        {
            public Transform Position;  // Array of waypoints
            public float Rotation;
        
        }

        [SerializeField] private Waypoints[] _waypoints;
        public float rotateSpeed = 1f; // Speed for rotation
        public float rotationAngle = 60f; // Rotation angle at waypoint
        private int currentWaypoint = 0;
    
        private bool isRotating = false;
        private bool isFirst = true;
        private bool hasStopped = false;
        private bool start = true;
        
        [SerializeField] private GameObject[] playerLight;
        [SerializeField] private GameObject[] ambientLight;
        
        [SerializeField] private CameraSwitcher cameraSwitcher;
        
        [SerializeField] private GameObject skipButton;

    
        void Start()
        {
            skipButton.SetActive(true);
        }
        
        void Update()
        {
            if (!isRotating && !hasStopped && _waypoints.Length > 0)
            {
                if (!isFirst)
                {
                    cameraSwitcher.PlaySwitchSound();
                }else{
                    isFirst = false;
                    GoToNextWaypoint();
                }
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    // Move to the next waypoint if not at the last
                    if(!start)
                        currentWaypoint++;
                    GoToNextWaypoint();
                }   
                if(Input.GetKeyDown(KeyCode.LeftArrow) )
                {
                    if (currentWaypoint>0 || !start){
                        // Move to the previous waypoint if not at the first
                        if (start)
                        {
                            currentWaypoint--;
                        }

                        GoToNextWaypoint(1);
                    }
                } 
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                skipButton.SetActive(false);
                StartLevel();
                
            }
        }
    
        void GoToNextWaypoint(int forward = 0)
        {
            //print("Traslazione :" + transform.position);
            //print("Rotazione :" + transform.rotation);
            if (currentWaypoint >= _waypoints.Length)
            {
                hasStopped = true;
                skipButton.SetActive(false);
                StartLevel(); // Stop when the last waypoint is reached
                return;
            }
    
            // Directly translate the camera to the next waypoint
            Transform target = _waypoints[currentWaypoint].Position;
            Vector3 direction = target.position - transform.position;
    
            // Translate instantly to the position of the next waypoint
            if(forward==0 && !start)
            {
                transform.Rotate(Vector3.up, _waypoints[currentWaypoint].Rotation, Space.World);
            }
            else if (forward==1 && start)
            {
                transform.Rotate(Vector3.up, -_waypoints[currentWaypoint+1].Rotation, Space.World);
            }
            
            transform.Translate(direction, Space.World);
            
            // Start rotating at the waypoint
            isRotating = true;
            if(forward==1)
            {
                StartCoroutine(BackRotateAtWaypoint());
                start = true;
            }
            else
            {
                StartCoroutine(RotateAtWaypoint());
                start = false;
            }
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
            yield return new WaitForSeconds(0.3f);
            isRotating = false;
            
        }
        
        System.Collections.IEnumerator BackRotateAtWaypoint()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -rotationAngle, 0));
            float time = 0;
    
            while (time < 1)
            {
                time += Time.deltaTime * rotateSpeed;
                transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
                yield return null;
            }
            yield return new WaitForSeconds(0.3f);
            isRotating = false;

            
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

