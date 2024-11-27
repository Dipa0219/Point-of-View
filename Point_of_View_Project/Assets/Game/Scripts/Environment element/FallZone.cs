using UnityEngine;

namespace Game.Scripts.Environment_element
{
    public class FallZone : MonoBehaviour
    {
        private bool _active;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collided ");
            Debug.Log("Collided with: " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Collided with Player");
                _active = true;
            }
        }
      
        public bool IsActive()
        {
            return _active;
        }
    }
}
