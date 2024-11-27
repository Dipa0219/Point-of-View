using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float speed;
        
        private List<Transform> _children;
        private SphereCollider _collider;
        private int _currentWaypoint = 0;
        private bool _isMoving = false;
        
        private void Awake()
        {
            _children = new List<Transform>();
            GetRecursiveChildren(transform);
            _collider = GetComponent<SphereCollider>();
        }
        
        private void GetRecursiveChildren(Transform parentTransform)
        {
            foreach (Transform child in parentTransform)
            {
                _children.Add(child.transform);
                if (child.transform.childCount > 0)
                {
                    GetRecursiveChildren(child);
                }
            }
        }

        private void Update()
        {
            if (_isMoving) return;
            GoToNextWaypoint();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("ENTER");
            GoToNextWaypoint();
            // TODO activate colliders in Base
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("EXIT");
        }
        
        private void GoToNextWaypoint()
        {
            // if (_currentWaypoint >= waypoints.Length)
            // {
            //     hasStopped = true;
            //     StartLevel(); // Stop when the last waypoint is reached
            // }
    
            // Directly translate the camera to the next waypoint
            // Transform target = waypoints[_currentWaypoint];
            // Vector3 direction = target.position - transform.position;
            //
            // // Translate instantly to the position of the next waypoint
            // transform.Translate(direction, Space.World);
            
            _currentWaypoint++;
        }
    }
}
