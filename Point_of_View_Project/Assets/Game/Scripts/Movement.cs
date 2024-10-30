using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody rb; // Riferimento al Rigidbody
    [SerializeField]private float forceAmount = 10f; // Valore della forza applicata
    [SerializeField]private float maxSpeed = 10f; // Valore della velocità massima
    private bool _isActive;

    void Start()
    {
        // Otteniamo il componente Rigidbody dell'oggetto
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        if(_isActive)
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
}
