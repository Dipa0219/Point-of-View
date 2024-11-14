using System;
using UnityEngine;

namespace Game.Scripts.Environment_element
{
   public class PressurePlateManager : MonoBehaviour
   {
      Renderer rend;
      Boolean _isActive;

      private void Start()
      {
         rend = GetComponent<Renderer>();
      }

      private void OnCollisionEnter(Collision other)
      {
         rend.material.color = Color.red;
         _isActive = true;
      }
   
   
      private void OnCollisionExit(Collision other)
      {
         rend.material.color = Color.white;
         _isActive = false;
      }

      public void EndGame()
      {
         rend.material.color = Color.green;
      }

      public Boolean isActive()
      {
         return _isActive;
      }
   }
}
