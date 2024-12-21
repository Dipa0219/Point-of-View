using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game.Scripts
{
    public class MovingPlatformUnbound : MonoBehaviour
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
        //[SerializeField] private bool isDouble;

        [SerializeField] private AudioClip soundEffect;
        private AudioSource _audioSource;

        private List<Transform> _children;

        //private BoxCollider[] _colliders;
        private Transform _playerTransform;
        private int _nextWaypoint = 1;
        private bool _move;
        private bool _stop;
        private bool _forward = true;
        private int _numPlayer;

        private bool isWaiting;
        [SerializeField] private float delay = 2f;

        private void Start()
        {
            _move = false;
            _stop = true;
            
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }

        private void Awake()
        {
            _children = new List<Transform>();
            //GetRecursiveChildren(transform);
            //_colliders = _children[0].GetComponents<BoxCollider>();
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


        public void startMoving()
        {
            GoToNextWaypoint();
        }

        public void stopMoving()
        {
            _stop = true;
            _move = false;

        }

        public void resetPlatform()
        {
            stopMoving();
            transform.position = waypoints[0].position;
        }

        //private void OnTriggerExit(Collider other)
        private void OnCollisionExit(Collision other)
        {
            if (!other.transform.CompareTag("Player")) return;
            //if (!other.CompareTag("Player")) return;
            other.transform.SetParent(null);
        }

        //private void OnTriggerEnter(Collider other)
        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Player")) return;
            //if (!other.CompareTag("Player")) return;
            other.transform.SetParent(transform);
        }

        private void GoToNextWaypoint()
        {
            if (!_move)
            {
                _audioSource.Play();
                _move = true;
                _stop = false;
            }
        }


        private void Update()
        {
            if (_stop)
            {
                // Deactivate colliders
                //foreach (BoxCollider boxCollider in _colliders)
                //   boxCollider.enabled = false;
                _audioSource.Stop();
            }
            
            if (_stop || !_move) return;
            
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_nextWaypoint].position,
                speed * Time.deltaTime);

            if (transform.position != waypoints[_nextWaypoint].position) return;

            if (!isWaiting)
                StartCoroutine(WaitAtWaypoint());

            /*
            switch (finalBehaviour)
            {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1)
                    {
                        _stop = true;

                    }
                    else
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
                        if (_nextWaypoint == waypoints.Length - 1)
                        {
                            _forward = false;
                            _nextWaypoint--;
                        }
                        else
                            _nextWaypoint++;
                    else if (_nextWaypoint == waypoints.Length - 1)
                    {
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
            */
            
            
            //foreach (BoxCollider boxCollider in _colliders)
            //    boxCollider.enabled = false;
        }

        IEnumerator WaitAtWaypoint()
        {
            isWaiting = true; // Stop movement
            yield return new WaitForSeconds(delay); // Wait for the delay duration
            
            isWaiting = false; // Resume movement
            
            switch (finalBehaviour)
            {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1)
                    {
                        _stop = true;

                    }
                    else
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
                        if (_nextWaypoint == waypoints.Length - 1)
                        {
                            _forward = false;
                            _nextWaypoint--;
                        }
                        else
                            _nextWaypoint++;
                    else if (_nextWaypoint == waypoints.Length - 1)
                    {
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

            //if (continuousMovement) return;
            if (continuousMovement) yield break;

            _move = false;
        }
    }
}
