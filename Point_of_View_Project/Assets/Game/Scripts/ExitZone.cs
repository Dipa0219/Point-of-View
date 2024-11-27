using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class ExitZone : MonoBehaviour
    {
        
        public List<Transform> children;
        private bool _active;

        void Awake()
        {
            children = new List<Transform>();
            GetRecursiveChildren(transform);
        }

        private void GetRecursiveChildren(Transform parentTransform)
        {
            foreach (Transform child in parentTransform)
            {
                children.Add(child.transform);
                if (child.transform.childCount > 0)
                {
                    GetRecursiveChildren(child);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("ENTER");
            children[0].transform.localScale = new Vector3(1, 1.6f, 1);
            _active = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("EXIT");
            children[0].transform.localScale = new Vector3(1, 0.6f, 1);
            _active = false;
        }
        
        public bool IsActive()
        {
            return _active;
        }
    }
}
