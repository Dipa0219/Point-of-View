using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class GetChildren : MonoBehaviour
    {
        public List<Transform> children;
        
        void Awake()
        {
            children = new List<Transform>();
            GetRecursiveChildren(transform);
            foreach (Transform child in children)
                Debug.Log(child.name);
            {
                
            }
        }
        
        // void Update()
        // {
        //
        // }

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
    }
}
