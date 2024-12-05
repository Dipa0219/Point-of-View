using UnityEngine;

namespace Game.Scripts
{
    public class ChangeColorTrigger : MonoBehaviour
    {
        [SerializeField] private Color color;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            transform.GetComponent<Light>().color = color;
        }
    }
}
