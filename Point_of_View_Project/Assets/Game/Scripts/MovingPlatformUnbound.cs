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
        [SerializeField] private bool continuousMovement = false; // Whether the platform stops at waypoints
        [SerializeField] private float delay = 2f;       // Delay at waypoints
        [SerializeField] private float detachDistance = 1.5f; // Max distance for player to remain attached
        [SerializeField] private AudioClip soundEffect;  // Sound effect for movement

        private AudioSource _audioSource;
        private Transform currentAttachedPlayer = null;  // Player currently on the platform
        private Vector3 playerOffset;                   // Offset of the player relative to the platform
        private int _nextWaypoint = 1;                  // Next waypoint index
        private bool _isMoving = false;
        private bool _isWaiting = false;
        private bool _forward = true;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (currentAttachedPlayer == null && other.CompareTag("Player"))
            {
                currentAttachedPlayer = other.transform;

                // Calculate initial offset
                playerOffset = currentAttachedPlayer.position - transform.position;

                // Notify player's movement script about the platform
                var playerMovement = currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.SetPlatform(transform);
                }

                Debug.Log("Player attached to platform.");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (currentAttachedPlayer == other.transform)
            {
                DetachPlayer();
            }
        }

        private void DetachPlayer()
        {
            if (currentAttachedPlayer != null)
            {
                // Notify the player's movement script about detachment
                var playerMovement = currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.ClearPlatform();
                }

                currentAttachedPlayer = null;
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
            if (currentAttachedPlayer != null)
            {
                var playerMovement = currentAttachedPlayer.GetComponent<Movement>();
                if (playerMovement != null)
                {
                    playerMovement.UpdatePlatformDelta(deltaMovement);
                }

                // Check detach distance
                float distanceFromCenter = Vector3.Distance(currentAttachedPlayer.position, transform.position);
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
                _audioSource.Play();
            }
        }

        public void StopMoving()
        {
            _isMoving = false;
            _audioSource.Stop();
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
