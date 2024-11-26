using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Game.Scripts
{
    public class ExitCircle : MonoBehaviour
    {
        
        public List<Transform> children;

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
            children[0].transform.localScale = new Vector3(1, 1.8f, 1);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("EXIT");
            children[0].transform.localScale = new Vector3(1, 0.6f, 1);
        }
        
        
    }
}
