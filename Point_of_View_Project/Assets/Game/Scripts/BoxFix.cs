using UnityEngine;

namespace Game.Scripts
{
    public class BoxFix : MonoBehaviour
    {
        [SerializeField] private float vertical = -1f;
        [SerializeField] private float speed = 10.0f;
        public LayerMask groundLayer; // LayerMask to detect the ground
        public int gridResolution = 5; // How many rays to cast along one axis
        public float rayDistance = 1f; // Distance to cast rays
        public float requiredSupportPercentage = 80f; // Minimum support percentage
        private BoxCollider boxCollider;

        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
        }
         private void Update()
         {
             if(ShouldFall())
             {
                 transform.position = Vector3.MoveTowards(transform.position,
                     transform.position + new Vector3(-0.1f, 0, 0), Time.deltaTime * speed);
             }         
         }
         
         bool ShouldFall()
         {
             Vector3 boxSize = boxCollider.size;
             Vector3 boxCenter = transform.position + boxCollider.center;

             int supportedPoints = 0;
             int totalPoints = gridResolution * gridResolution;

             // Iterate over a grid of points on the bottom surface
             for (int x = 0; x < gridResolution; x++)
             {
                 for (int z = 0; z < gridResolution; z++)
                 {
                     // Compute the local position of the raycast
                     float percentX = x / (float)(gridResolution - 1);
                     float percentZ = z / (float)(gridResolution - 1);

                     Vector3 localPoint = new Vector3(
                         Mathf.Lerp(-boxSize.x / 2, boxSize.x / 2, percentX),
                         -boxSize.y / 2, // Bottom of the box
                         Mathf.Lerp(-boxSize.z / 2, boxSize.z / 2, percentZ)
                     );

                     // Transform local point to world space
                     Vector3 worldPoint = boxCenter + transform.TransformVector(localPoint);

                     // Cast ray downward
                     if (Physics.Raycast(worldPoint, Vector3.down, rayDistance, groundLayer))
                     {
                         supportedPoints++;
                     }

                     // Visualize the rays in the editor
                     Debug.DrawRay(worldPoint, Vector3.down * rayDistance, Color.red);
                 }
             }

             // Calculate the percentage of supported points
             float supportPercentage = (supportedPoints / (float)totalPoints) * 100f;

             return supportPercentage < requiredSupportPercentage;
         }
    }
}
