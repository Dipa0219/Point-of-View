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
        [SerializeField] private float detachDistance = 1.5f;
        [SerializeField] private AudioClip soundEffect;
        private AudioSource _audioSource;
        
        private List<Transform> _children;
        private BoxCollider[] _colliders;
        private Transform _currentAttachedPlayer;
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
            
            //if(_numPlayer==0 && isDouble)
            if(isDouble)
            {
                if (_numPlayer == 0)
                {
                    _numPlayer++;
                    return;
                }
                
                _numPlayer++;
                GoToNextWaypoint();
                return;
            }
            
            if (_currentAttachedPlayer == null)
            {
                _currentAttachedPlayer = other.transform;


                // Notify player's movement script about the platform
                var playerMovement = _currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.SetPlatform(transform);
                }

                Debug.Log("Player attached to platform.");
            }
            
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
            if (!_move)
            {
                _audioSource.Play();
                _move = true;
            }
            
        }
        
        private void DetachPlayer()
        {
            if (_currentAttachedPlayer != null)
            {
                // Notify the player's movement script about detachment
                var playerMovement = _currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.ClearPlatform();
                }

                _currentAttachedPlayer = null;
                Debug.Log("Player detached from platform.");
            }
        }

        private void Update()
        {
            if (_stop)
            {
                // Deactivate colliders
                foreach (BoxCollider boxCollider in _colliders)
                    boxCollider.enabled = false;
                _audioSource.Stop();
            }
                
                
            if (_stop || !_move) return;

            Vector3 previousPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextWaypoint].position, speed * Time.deltaTime);

            // Calculate platform delta movement
            Vector3 deltaMovement = transform.position - previousPosition;

            // Update attached player's position based on delta
            if (_currentAttachedPlayer != null)
            {
                var playerMovement = _currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.UpdatePlatformDelta(deltaMovement);
                }

                // Check detach distance
                float distanceFromCenter = Vector3.Distance(_currentAttachedPlayer.position, transform.position);
                if (distanceFromCenter > detachDistance)
                {
                    DetachPlayer();
                }
            }

            if (transform.position != waypoints[_nextWaypoint].position) return;

            switch (finalBehaviour) {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1)
                    {
                        _stop = true;
                        
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
        }
    }
}
