using UnityEngine;

namespace Game.Scripts
{
    public class ChangePositionTrigger : MonoBehaviour
    {
    
        [SerializeField] private Transform objectToMove;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float speed;
        
        [SerializeField] private AudioClip soundEffect;
        private AudioSource _audioSource;

        private int _nextWaypoint;
        private bool _start;
        private bool _stop;

        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = soundEffect;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            //_start = true;
            if (!_start)
            {
                _audioSource.Play();
                _start = true;
            }
        }

         private void Update()
         {
             if (!_start || _stop){
                 _audioSource.Stop();
                 return;
             }
             
             if (objectToMove.position == waypoints[^1].position) {
                 _stop = true;
                 return;
             }
             
             if(objectToMove.position == waypoints[_nextWaypoint].position)
                 _nextWaypoint++;
             
             objectToMove.position = Vector3.MoveTowards(objectToMove.position, waypoints[_nextWaypoint].position, Time.deltaTime * speed);
         }
    }
}
