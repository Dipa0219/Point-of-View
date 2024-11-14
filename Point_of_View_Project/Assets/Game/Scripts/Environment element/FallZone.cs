using System;
using UnityEngine;

namespace Game.Scripts.Environment_element
{
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
    
       
        private void OnCollisionEnter(Collision collision)
        {
            print("Collided ");
            print("Collided with: " + collision.gameObject.tag);
            if (collision.gameObject.tag == "Player")
            {
                print("Collided with Player");
                _isActive = true;
            }
            //_isActive = true;
        }
   
      
        public Boolean isActive()
        {
            return _isActive;
        }
    }
}
