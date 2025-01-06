//using System.Collections.Generic;

using System.Collections;
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
        [SerializeField] private bool isDouble; // If it needs to wait for two robots and not just one
        
        [SerializeField] private AudioClip soundEffect;
        private AudioSource _audioSource;
        
        // private List<Transform> _children;
        // private BoxCollider[] _colliders;
        private Transform _playerTransform;
        private int _nextWaypointIndex = 1;
        private bool _move;
        private bool _stop;
        private bool _forward = true;
        // private int _numPlayer;
        
        private Transform _previousWaypoint;
        private Transform _nextWaypoint;
        private float _timeToWaypoint;
        private float _elapsedTime;
        
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }
        
        /*
        private void Awake()
        {
            _children = new List<Transform>();
            GetRecursiveChildren(transform);
            // _colliders = _children[0].GetComponents<BoxCollider>();
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
        */
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            // if(_numPlayer==0 && isDouble)
            // if(isDouble)
            // {
            //     if (_numPlayer == 0)
            //     {
            //         _numPlayer++;
            //         return;
            //     }
            //     
            //     _numPlayer++;
            //     GoToNextWaypoint();
            //     return;
            // }
            
            // Vector3 playerGlobalPosition = other.transform.position;
            Debug.Log("Player Transform Before Parenting: " + other.transform);
            //other.transform.localScale = new Vector3(4f, 1f, 4f);
            other.transform.SetParent(transform, true);
            //other.transform.localScale = new Vector3(4f, 4f, 4f);
            Debug.Log("Player Position After Parenting: " + other.transform.position);
            
            // _numPlayer++;
            
            // Activate colliders
            // foreach (BoxCollider boxCollider in _colliders)
            //     boxCollider.enabled = true;
            
            // Move
            StartCoroutine(DelayCoroutine());
            GoToNextWaypoint();
        }
        
         private void OnTriggerExit(Collider other)
         {
             if (!other.CompareTag("Player")) return;
             other.transform.SetParent(null);
             // _numPlayer--;
         }

        private void GoToNextWaypoint()
        {
            if (!_move && waypoints.Length > 0)
            {
                _audioSource.Play();
                CalcWaypointDistanceTime();
                _move = true;
            }
            
        }

        private void FixedUpdate()
        {
            if (_stop)
            {
                // Deactivate colliders
                // foreach (BoxCollider boxCollider in _colliders)
                //     boxCollider.enabled = false;
                _audioSource.Stop();
            }
                
                
            if (_stop || !_move) return;
            
            _elapsedTime += Time.deltaTime;
            float elapsedPercentage = _elapsedTime / _timeToWaypoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(_previousWaypoint.position, _nextWaypoint.position, elapsedPercentage);
            
            // transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextWaypointIndex].position, speed * Time.deltaTime);

            
            if (transform.position != waypoints[_nextWaypointIndex].position) return;

            switch (finalBehaviour) {
                case FinalBehaviour.Stop:
                    if (_nextWaypointIndex == waypoints.Length - 1)
                    {
                        _stop = true;
                        
                    }else
                        _nextWaypointIndex++;
                    break;
                case FinalBehaviour.Loop:
                    if (_nextWaypointIndex == waypoints.Length - 1)
                        _nextWaypointIndex = 0;
                    else
                         _nextWaypointIndex++;
                    break;
                case FinalBehaviour.Reverse:
                    if (_forward)
                        if (_nextWaypointIndex == waypoints.Length - 1) {
                            _forward = false;
                            _nextWaypointIndex--;
                        }
                        else
                             _nextWaypointIndex++;
                    else
                        if (_nextWaypointIndex == waypoints.Length - 1) {
                            _forward = false; 
                            _nextWaypointIndex--;
                        }
                        else
                            _nextWaypointIndex++;
                    break;
                
                default:
                    _stop = true;
                    break;
            }
            
            if (elapsedPercentage >= 1)
            {
                CalcWaypointDistanceTime();
            }
            
            if (continuousMovement) return;
            
            _move = false;
            
            // foreach (BoxCollider boxCollider in _colliders)
            //     boxCollider.enabled = false;
            
        }
        
        private void CalcWaypointDistanceTime()
        {
            _previousWaypoint = waypoints[_nextWaypointIndex-1];
            _nextWaypoint = waypoints[_nextWaypointIndex];

            _elapsedTime = 0;
            
            float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _nextWaypoint.position);
            _timeToWaypoint = distanceToWaypoint / speed;
        }

        private static IEnumerator DelayCoroutine()
        {
            // Aspetta 3 secondi
            yield return new WaitForSeconds(3f);
        }
    }
}
