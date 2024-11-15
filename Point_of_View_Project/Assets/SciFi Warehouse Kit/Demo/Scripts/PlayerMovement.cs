using UnityEngine;

namespace SciFi_Warehouse_Kit.Demo.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {

        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 8f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private AudioClip footStepSound;
        [SerializeField] private float footStepDelay;
        private CharacterController _characterController;
        private Vector3 _velocity;
        private bool _isGrounded;
        private bool _debugGroundCheck;
        private float _nextFootstep;

        private void Start() {
            _characterController = GetComponent<CharacterController>();
            
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
        }

        // Update is called once per frame
        private void Update() {
            
            _isGrounded = _characterController.isGrounded;
            
            if (_isGrounded && !_debugGroundCheck) {
                _debugGroundCheck = true;
                Debug.Log("Ground");
            }
            
            if (!_isGrounded && _debugGroundCheck) {
                _debugGroundCheck = false;
                Debug.Log("Air");
            }

            if (_isGrounded && _velocity.y < 0) { 
                _velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 motion = transform.right * x + transform.forward * z;
            
            // Horizontal speed
            controller.Move(motion * (speed * Time.deltaTime));

            // JUMP
            if(Input.GetButtonDown("Jump") /*&& _isGrounded*/) {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            // GRAVITY
            if (!_isGrounded) {
                _velocity.y += gravity * Time.deltaTime;
            }

            // Vertical speed
            controller.Move(_velocity * Time.deltaTime);

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && _isGrounded) {
                _nextFootstep -= Time.deltaTime;
                if (_nextFootstep <= 0) {
                    GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                    _nextFootstep += footStepDelay;
                }
            }
        }
    }
}


