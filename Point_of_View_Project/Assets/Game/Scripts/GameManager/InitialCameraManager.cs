using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GameManager
{
    public class InitialCameraManager : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float delay = 0.1f; // Delay time at each waypoint
        [SerializeField] private int startingPoint;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private GameObject target;
        
        [SerializeField] private CameraSwitcher cameraSwitcher;


        private int _i;
        private bool _isWaiting; // To check if the platform is waiting at a waypoint

        // Start is called before the first frame update
        void Start()
        {
            transform.position = waypoints[startingPoint].position;
            _i = startingPoint;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isWaiting)
            {
                // Move the platform if it's not waiting
                if (Vector3.Distance(transform.position, waypoints[_i].position) < 0.02f)
                {
                    StartCoroutine(WaitAtWaypoint()); // Start waiting at the waypoint
                }
                else
                {
                    // Move the platform towards the next waypoint
                    transform.position =
                        Vector3.MoveTowards(transform.position, waypoints[_i].position, speed * Time.deltaTime);
                }
            }
        }

        // Coroutine to handle waiting at waypoints
        IEnumerator WaitAtWaypoint()
        {
            _isWaiting = true; // Stop movement
            yield return new WaitForSeconds(delay); // Wait for the delay duration

            // Move to the next waypoint
            _i++;
            if (_i >= waypoints.Length)
            {
                cameraSwitcher.SwitchCameras();
                _i = 0; // Loop back to the first waypoint
            }

            _isWaiting = false; // Resume movement
        }
    }

}

