using UnityEngine;

namespace Game.Scripts.Environment_element.Platforms
{
    public class SpinningPlatform : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0f, 20f *Time.deltaTime, 0f, Space.Self);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            collision.transform.SetParent(transform);
        }
        private void OnCollisionExit(Collision collision)
        {
            collision.transform.SetParent(null);
        }
    }
}
