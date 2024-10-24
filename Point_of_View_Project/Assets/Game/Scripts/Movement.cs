using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int dir;
    private bool _isActive;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/
      
        float fastSpeed = speed * 2f;
        if (_isActive)
        {
            // Horizontal Movement
            Vector3 input = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(input * (dir * (fastSpeed * Time.deltaTime)));
            else
                transform.Translate(input * (dir * (speed * Time.deltaTime)));
        }

        
    }

    public void SetActive(bool active)
    {
        _isActive = active;
    }
}
