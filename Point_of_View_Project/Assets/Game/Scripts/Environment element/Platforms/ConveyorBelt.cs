using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Environment_element.Platforms
{
    public class ConveyorBelt : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector3 direction;
        [SerializeField] private List<GameObject> onBelt;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            for(int i = 0; i <= onBelt.Count -1; i++)
            {
                onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction;
            }
        }

        // When something collides with the belt
        private void OnCollisionEnter(Collision collision)
        {
            onBelt.Add(collision.gameObject);
        }

        // When something leaves the belt
        private void OnCollisionExit(Collision collision)
        {
            onBelt.Remove(collision.gameObject);
        }
    }
}