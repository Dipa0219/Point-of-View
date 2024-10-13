using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         float distance = Vector3.Distance(transform.position, other.transform.position);
         print("Distance:"+ distance );
      }
   }
}
