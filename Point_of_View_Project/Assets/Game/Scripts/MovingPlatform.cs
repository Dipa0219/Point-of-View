using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float speed;
        
        private List<Transform> _children;
        private BoxCollider[] _colliders;
        private Transform _playerTransform;
        private int _currentWaypoint;
        private bool _canMove;
        
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
            _playerTransform = other.transform;
            
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
            _canMove = true;
        }

        private void Update()
        {
            if (!_canMove || _currentWaypoint >= waypoints.Length) return;
            
            // Debug.Log("MOVING");
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypoint + 1].position, speed * Time.deltaTime);
            
            if (transform.position == waypoints[_currentWaypoint + 1].position)
            {
                if (_currentWaypoint == 0)
                    _currentWaypoint--;
                else
                    _currentWaypoint++;
                
                _canMove = false;
                
                // Deactivate colliders
                foreach (BoxCollider boxCollider in _colliders)
                    boxCollider.enabled = false;
            }
        }
    }
}
