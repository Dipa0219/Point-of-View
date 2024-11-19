using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
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
    private bool _isActive;

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

        if (_isGrounded && !_debugGroundCheck)
        {
            _debugGroundCheck = true;
            Debug.Log("Ground");
        }

        if (!_isGrounded && _debugGroundCheck)
        {
            _debugGroundCheck = false;
            Debug.Log("Air");
        }

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 motion = transform.right * x + transform.forward * z;
        
        if(_isActive)
        {
            // Horizontal speed
            controller.Move(motion * (speed * Time.deltaTime));
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.W) && _isGrounded)
            {
                _nextFootstep -= Time.deltaTime;
                if (_nextFootstep <= 0)
                {
                    GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                    _nextFootstep += footStepDelay;
                }
            }
        }
        

        // JUMP
        if (Input.GetButtonDown("Jump") /*&& _isGrounded*/)
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
   //private Rigidbody rb; // Riferimento al Rigidbody
   //[SerializeField] private float forceAmount = 10f; // Valore della forza applicata
   //[SerializeField] private float maxSpeed = 15f; // Valore della forza applicata
   //[SerializeField] private float direction = 1.0f;
   //private bool _isActive;

   //void Start()
   //{
   //    // Otteniamo il componente Rigidbody dell'oggetto
   //    rb = GetComponent<Rigidbody>();
   //    rb.maxLinearVelocity = maxSpeed;
   //}

   //void Update()
   //{
   //    var isMoving = false;
   //    if (!_isActive)
   //    {
   //        rb.velocity = Vector3.zero;
   //        return;
   //    }
   //    // Aggiungiamo una forza a sinistra se premiamo A
   //    if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
   //    {
   //        rb.AddForce(transform.rotation * Vector3.left * (forceAmount * direction), ForceMode.Force);
   //        isMoving = true;
   //    }

   //    // Aggiungiamo una forza a destra se premiamo D
   //    if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
   //    {
   //        isMoving = true;
   //        rb.AddForce(transform.rotation * Vector3.right * (forceAmount * direction), ForceMode.Force);
   //    }

   //    // Aggiungiamo una forza verso l'alto se premiamo W
   //    if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
   //    {
   //        isMoving = true;
   //        rb.AddForce(transform.rotation * Vector3.forward * (forceAmount * direction), ForceMode.Force);
   //    }

   //    // Aggiungiamo una forza verso il basso se premiamo S
   //    if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
   //    {
   //        isMoving = true;
   //        rb.AddForce(transform.rotation * Vector3.back * (forceAmount * direction), ForceMode.Force);
   //    }

   //    if (!isMoving)
   //    {
   //        rb.velocity = Vector3.zero;
   //    }


   //}

    public void SetActive(bool active)
    {
        _isActive = active;
    }

}
