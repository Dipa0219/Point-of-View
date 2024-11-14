using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Rigidbody rb; // Riferimento al Rigidbody
    [SerializeField] private float forceAmount = 10f; // Valore della forza applicata
    [SerializeField] private float maxSpeed = 15f; // Valore della forza applicata
    [SerializeField] private float direction = 1.0f;
    private bool _isActive;

    void Start()
    {
        // Otteniamo il componente Rigidbody dell'oggetto
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        var isMoving = false;
        if (!_isActive)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        // Aggiungiamo una forza a sinistra se premiamo A
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(transform.rotation * Vector3.left * (forceAmount * direction), ForceMode.Force);
            isMoving = true;
        }

        // Aggiungiamo una forza a destra se premiamo D
        if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            rb.AddForce(transform.rotation * Vector3.right * (forceAmount * direction), ForceMode.Force);
        }

        // Aggiungiamo una forza verso l'alto se premiamo W
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            rb.AddForce(transform.rotation * Vector3.forward * (forceAmount * direction), ForceMode.Force);
        }

        // Aggiungiamo una forza verso il basso se premiamo S
        if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            rb.AddForce(transform.rotation * Vector3.back * (forceAmount * direction), ForceMode.Force);
        }

        if (!isMoving)
        {
            rb.velocity= Vector3.zero;
        }


        
    }

    public void PhysicsUpdate()
    {
        
    }

    public void SetActive(bool active)
    {
        _isActive = active;
    }

}
