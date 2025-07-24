using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    public float topClamp = -90f;
    public float bottomClamp = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting the mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotation around the X axis (Look up and down)
        xRotation -= mouseY;

        //Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotaion around the y axis (Look left and right)
        yRotation += mouseX;

        //Apply rotations to our transform
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);


    }
}
