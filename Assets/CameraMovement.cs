using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity;
    public float speed;
    public int dir;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float vertical = 0f;
        float fastSpeed = speed * 2f;
        Vector3 movement = Vector3.zero;
        Vector3 input = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
            vertical = speed * 0.005f;

        if (Input.GetKey(KeyCode.LeftControl))
            vertical = -speed * 0.005f;

        // Horizontal Movement
        if (Input.GetKey(KeyCode.LeftShift))
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(input * (dir * (fastSpeed * Time.deltaTime)));
        }
        else
        {
            input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.TransformDirection(input * (dir * (speed * Time.deltaTime)));
        }
        // Vertical movement
        transform.Translate(movement + Vector3.up * vertical, Space.World);

        
        // Rotation
        if (Input.GetMouseButton(1)) //if we are holding right click
        {
            Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            transform.Rotate(mouseInput * sensitivity);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
        }
    }

}
