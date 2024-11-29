using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        private enum FinalBehaviour
        {
            Stop,
            Loop,
            Reverse
        }
        
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float speed;
        [SerializeField] private FinalBehaviour finalBehaviour;
        
        private List<Transform> _children;
        private BoxCollider[] _colliders;
        // private Transform _playerTransform;
        private int _currentWaypoint;
        private int _nextWaypoint = 1;
        private bool _move;
        private bool _stop;
        private bool _forward = true;
        
        private void Awake()
        {
            _children = new List<Transform>();
            GetRecursiveChildren(transform);
            _colliders = _children[0].GetComponents<BoxCollider>();
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("ENTER");
            
            // Take transform of the player
            //_playerTransform = other.transform;
            
            // Activate colliders
            foreach (BoxCollider boxCollider in _colliders)
                boxCollider.enabled = true;
            
            // Move
            GoToNextWaypoint();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("EXIT");
        }

        private void GoToNextWaypoint()
        {
            _move = true;
        }

        private void Update()
        {
            if (_stop)
                // Deactivate colliders
                foreach (BoxCollider boxCollider in _colliders)
                    boxCollider.enabled = false;
            
            if (_stop || !_move || _currentWaypoint >= waypoints.Length) return;
            
            // Debug.Log("MOVING");
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextWaypoint].position, speed * Time.deltaTime);

            if (transform.position != waypoints[_currentWaypoint + 1].position) return;

            // Deactivate colliders
            switch (finalBehaviour) {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1)
                        _stop = true;
                    else {
                        _currentWaypoint++; _nextWaypoint++;
                    }
                    break;
                case FinalBehaviour.Loop:
                    if (_nextWaypoint == waypoints.Length - 1)
                        _nextWaypoint = 0;
                    else {
                        _currentWaypoint++; _nextWaypoint++;
                    }
                    break;
                case FinalBehaviour.Reverse:
                    if (_forward)
                        if (_nextWaypoint == waypoints.Length - 1) {
                            _forward = false;
                            _currentWaypoint++; _nextWaypoint--;
                        }
                        else {
                            _currentWaypoint++; _nextWaypoint++;
                        }
                    else {
                        if (_nextWaypoint == waypoints.Length - 1) {
                            _forward = false;
                            _currentWaypoint++; _nextWaypoint--;
                        }
                        else {
                            _currentWaypoint++; _nextWaypoint++;
                        }
                    }
                    break;
                
                default:
                    _stop = true;
                    break;
            }

            foreach (BoxCollider boxCollider in _colliders)
                boxCollider.enabled = false;
        }
    }
}
