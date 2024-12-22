using UnityEngine;

namespace Game.Scripts
{
    public class Movement_Edit : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;

        private Vector3 _velocity;
        private bool _isGrounded;
        private bool _isActive;

        private Transform _platform;
        private Vector3 _platformDeltaMovement;

        private void Update()
        {
            _isGrounded = controller.isGrounded;

            // Reset vertical speed when grounded
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            // Get input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            //Vector3 motion = transform.right * x + transform.forward * z;
            Vector3 motion = - transform.right * x - transform.forward * z;

            if (_isActive)
            {
                // Apply horizontal movement
                controller.Move((motion * (speed * Time.deltaTime)) + _platformDeltaMovement);
            }

            // Jump
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // Apply gravity
            if (!_isGrounded)
            {
                _velocity.y += gravity * Time.deltaTime;
            }

            // Apply vertical movement
            controller.Move(_velocity * Time.deltaTime);

            // Reset platform delta movement after applying it
            _platformDeltaMovement = Vector3.zero;
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }

        public void SetPlatform(Transform platform)
        {
            _platform = platform;
        }

        public void UpdatePlatformDelta(Vector3 deltaMovement)
        {
            _platformDeltaMovement = deltaMovement;
        }

        public void ClearPlatform()
        {
            _platform = null;
            _platformDeltaMovement = Vector3.zero;
        }
    }
}
