using UnityEngine;

namespace Game.Scripts
{
    public class BoxFix : MonoBehaviour
    {
        [SerializeField] private float vertical = 0.001f;
        [SerializeField] private float verticalSpeed = 0.0025f;
        [SerializeField] private float speed = 10.0f;
        private BoxCollider boxCollider;

        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
         private void Update()
         {
             transform.position = Vector3.MoveTowards(transform.position,
                 transform.position + new Vector3(0, vertical, 0), Time.deltaTime * verticalSpeed);     
         }
         
    }
}
