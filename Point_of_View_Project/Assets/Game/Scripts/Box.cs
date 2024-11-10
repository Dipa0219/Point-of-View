using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Rigidbody rb; // Riferimento al Rigidbody
    [SerializeField] private LayerMask groundLayer; // Layer del terreno
    [SerializeField] private LayerMask boxLayer; // Layer della scatola
    [SerializeField] private float detectionDistance = 2f;
    [SerializeField] private Vector3 boxSize = new Vector3(5f, 4f, 5f);
    private bool _isGrounded;
    private bool _isOnABox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //_isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 8f, groundLayer); //questo funziona
        Vector3 up = new Vector3(0, 3f, 0);
        _isGrounded = Physics.BoxCast(transform.position + up, boxSize / 2, Vector3.down, Quaternion.identity, detectionDistance, groundLayer);
        // Controlliamo se l'oggetto è a contatto con una scatola
        //_isOnABox = Physics.Raycast(transform.position, Vector3.down, 3f, boxLayer); //questo funziona
        _isOnABox = Physics.BoxCast(transform.position + up, boxSize / 2, Vector3.down, Quaternion.identity, detectionDistance, boxLayer);
        print("il ground:  " + _isGrounded);
        //print(_isOnABox);

       if (!(_isGrounded || _isOnABox))
       {
           rb.AddForce(Vector3.down * 300f, ForceMode.Force);
           rb.velocity = new Vector3(0, rb.velocity.y, 0);
           rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                            RigidbodyConstraints.FreezeRotation;
       }
       else
       {
           rb.constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);
       }
    }
    
    void OnDrawGizmos()
    {
        if (rb != null)
        {
            Vector3 boxOrigin = transform.position;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(boxOrigin - Vector3.up * detectionDistance / 2, boxSize);
        }
    }
    
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    
    //    // Calcola la destinazione del raggio
    //    Vector3 rayEnd = transform.position + Vector3.down.normalized * 8f;
//
    //    // Disegna il Raycast nella scena
    //    Gizmos.DrawLine(transform.position, rayEnd);
//
    //    // Opzionale: se colpisce un oggetto, disegna una sfera nel punto di collisione
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit, 8f, groundLayer))
    //    {
    //        Gizmos.color = Color.red; // Cambia colore per il punto di collisione
    //        Gizmos.DrawSphere(hit.point, 0.1f); // Visualizza il punto di collisione
    //    }
    //}
}
