using System;
using System.Timers;
using UnityEngine;

namespace Game.Scripts
{
    public class Hovering : MonoBehaviour
    {
        [SerializeField] private float amplitude;
        [SerializeField] private float speed;
        private float _timer = 0f;

        private void Update()
        {
            _timer += Time.deltaTime;
            transform.Translate(0, Mathf.Sin(_timer * speed / 2 * Mathf.PI) * amplitude / 1000, 0);
        }
    }
}