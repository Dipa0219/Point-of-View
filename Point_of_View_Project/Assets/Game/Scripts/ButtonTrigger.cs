using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Scripts
{
    public class ButtonTrigger : MonoBehaviour
    {
    
        [SerializeField] private Transform objectToMove;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float speed;

        private int _nextWaypoint = 1;
        private bool _start;
        private bool _stop;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            _start = true;
        }

         private void Update()
         {
             if (!_start || _stop) return;
             
             if(objectToMove.position == waypoints[waypoints.Length - 1].position)
                 _stop = true;
             
             if(objectToMove.position == waypoints[_nextWaypoint].position)
                 _nextWaypoint++;
             
             objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypoints[_nextWaypoint].position, Time.deltaTime * speed);
         }
    }
}
