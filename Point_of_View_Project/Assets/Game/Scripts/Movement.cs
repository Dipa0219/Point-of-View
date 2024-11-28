using UnityEngine;

namespace Game.Scripts
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;
        // [SerializeField] private AudioClip footStepSound;
        // [SerializeField] private float footStepDelay;
        private CharacterController _characterController;
        private Vector3 _velocity;
        private bool _isGrounded;
        private bool _debugGroundCheck;
        private float _nextFootstep;
        private bool _isActive;
        // private AudioSource _audioSource;

        private void Start() {
            _characterController = GetComponent<CharacterController>();
            // _audioSource = GetComponent<AudioSource>();

            // DA TENERE
            /*
            if (_characterController != null) {
                Debug.Log("CharacterController found!");
                Debug.Log("Radius: " + _characterController.radius);
                Debug.Log("Height: " + _characterController.height);
                Debug.Log("Center: " + _characterController.center);
            }
            else
            {
                Debug.Log("CharacterController not found.");
            }
            */
        }

        private void Update() {
            _isGrounded = _characterController.isGrounded;

            if (_isGrounded && !_debugGroundCheck)
            {
                _debugGroundCheck = true;
                //Debug.Log("Ground");
            }

            if (!_isGrounded && _debugGroundCheck)
            {
                _debugGroundCheck = false;
                //Debug.Log("Air");
            }
            
            // Reset vertical speed
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 motion = - transform.right * x - transform.forward * z;
        
            if(_isActive)
            {
                // Horizontal speed
                controller.Move(motion * (speed * Time.deltaTime));
            
                // if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                //     Input.GetKey(KeyCode.W) && _isGrounded)
                // {
                //     _nextFootstep -= Time.deltaTime;
                //     if (_nextFootstep <= 0)
                //     {
                //         _audioSource.PlayOneShot(footStepSound, 0.7f);
                //         _nextFootstep += footStepDelay;
                //     }
                // }
            }

            // JUMP
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // GRAVITY
            if (!_isGrounded)
            {
                _velocity.y += gravity * Time.deltaTime;
            }

            // Vertical speed
            controller.Move(_velocity * Time.deltaTime);
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }

    }
}
