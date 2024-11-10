using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb; // Riferimento al Rigidbody
    [SerializeField] private float forceAmount = 10f; // Valore della forza applicata
    [SerializeField] private float maxSpeed = 10f; // Valore della forza applicata
    [SerializeField] private LayerMask groundLayer; // Layer del terreno
    [SerializeField] private LayerMask boxLayer; // Layer della scatola
    [SerializeField] private Vector3 boxSize = new Vector3(5f, 4f, 5f);
    [SerializeField] private float detectionDistance = 2f;
    private bool _isGrounded;
    private bool _isOnABox;
    private bool _isActive;

    void Start()
    {
        // Otteniamo il componente Rigidbody dell'oggetto
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        
        // Controlliamo se l'oggetto è a contatto con il terreno
        //_isGrounded = Physics.Raycast(transform.position, Vector3.down, 2.5f, groundLayer); questo funziona
        _isGrounded = Physics.BoxCast(transform.position, boxSize / 2, Vector3.down, Quaternion.identity, detectionDistance, groundLayer);
        // Controlliamo se l'oggetto è a contatto con una scatola
        //_isOnABox = Physics.Raycast(transform.position, Vector3.down, 2.6f, boxLayer); questo funziona
        _isOnABox = Physics.BoxCast(transform.position, boxSize / 2, Vector3.down, Quaternion.identity, detectionDistance, boxLayer);
        //print(_isGrounded);
        //print("la box: " + _isOnABox);
        
        if (_isActive && (_isGrounded || _isOnABox))
        {
            // Aggiungiamo una forza a sinistra se premiamo A
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(Vector3.left * forceAmount, ForceMode.Force);

            // Aggiungiamo una forza a destra se premiamo D
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(Vector3.right * forceAmount, ForceMode.Force);

            // Aggiungiamo una forza verso l'alto se premiamo W
            if (Input.GetKey(KeyCode.W))
                rb.AddForce(Vector3.forward * forceAmount, ForceMode.Force);

            // Aggiungiamo una forza verso il basso se premiamo S
            if (Input.GetKey(KeyCode.S))
                rb.AddForce(Vector3.back * forceAmount, ForceMode.Force);
        }
    }

    public void SetActive(bool active)
    {
        _isActive = active;
    }
    
    // Visualizza il BoxCast nell'editor per il debug
    void OnDrawGizmos()
    {
        if (rb != null)
        {
            Vector3 boxOrigin = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxOrigin - Vector3.up * detectionDistance / 2, boxSize);
        }
    }
}