using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public int dir;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        float fastSpeed = speed * 2f;

        // Horizontal Movement
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.LeftShift))
            transform.Translate(input * (dir * (fastSpeed * Time.deltaTime)));
        else
            transform.Translate(input * (dir * (speed * Time.deltaTime)));
    }

}
