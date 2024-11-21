using System;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject darkSparks;
    [SerializeField] private GameObject sparks;
    [SerializeField] private GameObject sides;
    [SerializeField] private GameObject lightX;

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            sides.transform.localScale = new Vector3(1, 1, 1);
        }

    }


    // void Update()
    // {
    //     
    // }
}
