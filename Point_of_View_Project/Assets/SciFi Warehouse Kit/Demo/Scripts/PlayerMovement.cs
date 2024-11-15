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
        private Vector3 _velocity;
        private bool _isGrounded;
        
        [SerializeField] private AudioClip footStepSound;
        [SerializeField] private float footStepDelay;
 
        private float _nextFootstep;

        // Update is called once per frame
        private void Update() {
            
            //_isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            _isGrounded = _velocity.y == 0;
            
            if (_isGrounded) {
                Debug.Log("ground check");
            }
            else {
                Debug.Log("Oof");
            }

            if (_isGrounded && _velocity.y <0) {
                _velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 motion = transform.right * x + transform.forward * z;
            controller.Move(motion * (speed * Time.deltaTime));

            if(Input.GetButtonDown("Jump") && _isGrounded) {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (!_isGrounded) {
                _velocity.y += gravity * Time.deltaTime;
            }

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


