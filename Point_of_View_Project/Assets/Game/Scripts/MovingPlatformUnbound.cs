using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private Transform[] waypoints;   // Waypoints for platform movement
        [SerializeField] private float speed = 3f;       // Platform speed
        [SerializeField] private FinalBehaviour finalBehaviour;
        [SerializeField] private bool continuousMovement; // Whether the platform stops at waypoints
        [SerializeField] private float delay = 2f;       // Delay at waypoints
        [SerializeField] private float detachDistance = 1.5f; // Max distance for player to remain attached
        [SerializeField] private AudioClip soundEffect;  // Sound effect for movement

        private AudioSource _audioSource;
        private Transform _currentAttachedPlayer;  // Player currently on the platform
        private int _nextWaypoint = 1;                  // Next waypoint index
        private bool _isMoving;
        private bool _isWaiting;
        private bool _forward = true;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_currentAttachedPlayer == null && other.CompareTag("Player"))
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
        }

        private void OnTriggerExit(Collider other)
        {
            if (_currentAttachedPlayer == other.transform)
            {
                print("exit");
                DetachPlayer();
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
            if (_isWaiting || !_isMoving) return;

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

            // Handle waypoint arrival
            if (transform.position == waypoints[_nextWaypoint].position)
            {
                StartCoroutine(WaitAtWaypoint());
            }
        }

        public void StartMoving()
        {
            if (!_isMoving)
            {
                _isMoving = true;
                if(_audioSource != null)
                    _audioSource.Play();
            }
        }

        public void StopMoving()
        {
            _isMoving = false;
            _audioSource.Stop();
        }
        
        
        public void SwitchWaypoints(Transform[] waypointsToBeRemoved, Transform[] waypointsToBeAdded)
        {
            List<Transform> updatedWaypoints = new List<Transform>(waypointsToBeAdded);
            foreach (var waypoint in waypoints)
            {
                if (!((IList<Transform>)waypointsToBeRemoved).Contains(waypoint))
                {
                    updatedWaypoints.Add(waypoint);
                }
            }
            waypoints = updatedWaypoints.ToArray();
        }
        

        private IEnumerator WaitAtWaypoint()
        {
            _isWaiting = true;
            yield return new WaitForSeconds(delay);

            // Decide next waypoint
            switch (finalBehaviour)
            {
                case FinalBehaviour.Stop:
                    if (_nextWaypoint == waypoints.Length - 1) StopMoving();
                    else _nextWaypoint++;
                    break;

                case FinalBehaviour.Loop:
                    _nextWaypoint = (_nextWaypoint + 1) % waypoints.Length;
                    break;

                case FinalBehaviour.Reverse:
                    if (_forward)
                    {
                        if (_nextWaypoint == waypoints.Length - 1) _forward = false;
                        else _nextWaypoint++;
                    }
                    else
                    {
                        if (_nextWaypoint == 0) _forward = true;
                        else _nextWaypoint--;
                    }
                    break;
            }

            _isWaiting = false;

            if (continuousMovement) _isMoving = true;
        }
    }
}
