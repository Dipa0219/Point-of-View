using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class TpZone : MonoBehaviour
    {
        
        [SerializeField] private Transform target;
        
        private List<Transform> _children;
        private Transform _playerTransform;
        private CharacterController _characterController;
        private readonly Vector3 _yShift = new (0, 1.67f, 0);
        private bool _tpActive;

        private void Start()
        {
            _tpActive = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Debug.Log("enter: " + other.name + " into " + this.name);
            _playerTransform = other.transform;
            _characterController = other.GetComponent<CharacterController>();

            if (!_characterController || !_tpActive) return;
            
            _characterController.enabled = false;
            target.GetComponent<TpZone>().SetTpActive(false);
            _playerTransform.position = target.position + _yShift;
            _characterController.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _tpActive = true;
            Debug.Log("exit: " + other.name + " from " + this.name);
        }

        public void SetTpActive(bool value)
        {
            _tpActive = value;
        }
    }
}
