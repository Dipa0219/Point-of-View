using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallZone : MonoBehaviour
{
    Boolean _isActive;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
       
   private void OnCollisionEnter(Collision other)
   {
        _isActive = true;
   }
   
      
   public Boolean isActive()
   {
       return _isActive;
   }
}
