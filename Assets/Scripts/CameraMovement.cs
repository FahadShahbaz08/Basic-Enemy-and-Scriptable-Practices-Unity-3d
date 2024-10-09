using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 100f; // Mouse sensitivity
    public Transform target; // The object or position to rotate around (optional)

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Update rotation based on mouse movement
        rotationX -= mouseY; // Invert Y-axis for natural camera control
        rotationY += mouseX;

        // Clamp the X rotation so the camera doesn't flip upside down
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Rotate the camera
        if (target == null)
        {
            // Free look camera (rotate the camera around itself)
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        }
        else
        {
        }
    }
}
