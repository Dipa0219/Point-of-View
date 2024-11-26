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
            foreach (Transform child in children)
                Debug.Log(child.name);
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
            if(other.CompareTag("Player"))
                Debug.Log("player entered circle");
            Debug.Log("Entered exit circle");
        }
    }
}
