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
        [SerializeField] private bool continuousMovement;
        [SerializeField] private bool isDouble;
        
        [SerializeField] private AudioClip soundEffect;
        private AudioSource _audioSource;
        
        private List<Transform> _children;
        private BoxCollider[] _colliders;
        private Transform _playerTransform;
        private int _nextWaypoint = 1;
        private bool _move;
        private bool _stop;
        private bool _forward = true;
        private int _numPlayer;
        
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }
        
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
            
            if(_numPlayer==0 && isDouble)
            {
                _numPlayer++;
                return;
            }
            // Take transform of the player
            //_playerTransform = other.transform;
            //_playerTransform.SetParent(transform);
            _numPlayer++;
            // Activate colliders
            foreach (BoxCollider boxCollider in _colliders)
                boxCollider.enabled = true;
            
            // Move
            GoToNextWaypoint();
        }
        
         private void OnTriggerExit(Collider other)
         {
             if (!other.CompareTag("Player")) return;

             _numPlayer--;
         }

        private void GoToNextWaypoint()
        {
            _audioSource.Play();
            _move = true;
        }

        private void Update()
        {
            if (_stop)
                // Deactivate colliders
                foreach (BoxCollider boxCollider in _colliders)
                    boxCollider.enabled = false;
            
            if (_stop || !_move) return;
            
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextWaypoint].position, speed * Time.deltaTime);

            if (transform.position != waypoints[_nextWaypoint].position) return;

            switch (finalBehaviour) {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1)
                    {
                        _stop = true;
                        _audioSource.Stop();
                    }else
                        _nextWaypoint++;
                    break;
                case FinalBehaviour.Loop:
                    if (_nextWaypoint == waypoints.Length - 1)
                        _nextWaypoint = 0;
                    else
                         _nextWaypoint++;
                    break;
                case FinalBehaviour.Reverse:
                    if (_forward)
                        if (_nextWaypoint == waypoints.Length - 1) {
                            _forward = false;
                            _nextWaypoint--;
                        }
                        else
                             _nextWaypoint++;
                    else
                        if (_nextWaypoint == waypoints.Length - 1) {
                            _forward = false; 
                            _nextWaypoint--;
                        }
                        else
                            _nextWaypoint++;
                    break;
                
                default:
                    _stop = true;
                    break;
            }
            
            if (continuousMovement) return;
            
            _move = false;
            
            foreach (BoxCollider boxCollider in _colliders)
                boxCollider.enabled = false;
            
            
        }
    }
}
