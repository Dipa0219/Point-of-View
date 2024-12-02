using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class ExitZone : MonoBehaviour
    {
        
        private List<Transform> _children;
        private bool _active;

        private void Awake()
        {
            _children = new List<Transform>();
            GetRecursiveChildren(transform);
        }

        private void GetRecursiveChildren(Transform parentTransform)
        {
            foreach (Transform child in parentTransform)
            {
                _children.Add(child.transform);
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
            _children[0].transform.localScale = new Vector3(1, 1.6f, 1);
            //_children[0].GetComponent<Renderer>().material.color = Color.green;
            //_children[0].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            _active = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("EXIT");
            _children[0].transform.localScale = new Vector3(1, 0.6f, 1);
            _active = false;
        }
        
        public bool IsActive()
        {
            return _active;
        }
    }
}
