using UnityEngine;

namespace Game.Scripts.Environment_element
{
    public class FallZone : MonoBehaviour
    {
        private bool _active;

        [SerializeField] private AudioClip soundEffect_game_over; 
        private AudioSource _audioSource_game_over;
        [SerializeField] private AudioClip soundEffect_box; 
        private AudioSource _audioSource_box;
        
        private void Start()
        {
            _audioSource_game_over = gameObject.AddComponent<AudioSource>();
            _audioSource_game_over.clip = soundEffect_game_over;
            _audioSource_box = gameObject.AddComponent<AudioSource>();
            _audioSource_box.clip = soundEffect_box;
        }
        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log("Collided ");
            // Debug.Log("Collided with: " + other.gameObject.tag);
            if (other.gameObject.CompareTag("Player"))
            {
                // Debug.Log("Collided with Player");
                _audioSource_game_over.Play();
                _active = true;
            }else if (other.gameObject.CompareTag("Pushable")) {
                _audioSource_box.Play();
            }
        }
      
        public bool IsActive()
        {
            return _active;
        }
    }
}
