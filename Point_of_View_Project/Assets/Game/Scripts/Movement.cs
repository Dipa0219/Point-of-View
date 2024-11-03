using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb; // Riferimento al Rigidbody
    [SerializeField] private float forceAmount = 10f; // Valore della forza applicata
    [SerializeField] private float maxSpeed = 15f; // Valore della forza applicata
    private bool _isActive;

    void Start()
    {
        // Otteniamo il componente Rigidbody dell'oggetto
        print(transform.position+ ", " + transform.rotation);
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        bool _isMoving = false;
        if(_isActive)
        {
            // Aggiungiamo una forza a sinistra se premiamo A
            if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(transform.rotation * Vector3.left  * forceAmount , ForceMode.Force);
                _isMoving = true;
            }

            // Aggiungiamo una forza a destra se premiamo D
            if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
            {
                _isMoving = true;
                rb.AddForce(transform.rotation * Vector3.right * forceAmount, ForceMode.Force);
            }

            // Aggiungiamo una forza verso l'alto se premiamo W
            if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
            {
                _isMoving = true;
                rb.AddForce(transform.rotation * Vector3.forward * forceAmount, ForceMode.Force);
            }

            // Aggiungiamo una forza verso il basso se premiamo S
            if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow))
            {
                _isMoving = true;
                rb.AddForce(transform.rotation * Vector3.back * forceAmount, ForceMode.Force);
            }

            if (!_isMoving)
            {
                rb.velocity= Vector3.zero;
            }
        }

        
    }
    
    public void SetActive(bool active)
    {
        _isActive = active;
    }

}
