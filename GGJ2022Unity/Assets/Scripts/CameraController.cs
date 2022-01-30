using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float CAM_X_MAX = 50f;
    const float CAM_X_MIN = -50f;

    public Transform playerRef;    

    public float mouseXSensitivity = 1.5f;
    public float mouseYSensitivity = 1.0f;

    public float currentXRotation;
    public float currentYRotation;

    float yRotation = 0f;

    private void Awake()
    {
        currentXRotation = 0;
        currentYRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentXRotation = Input.GetAxis("Mouse X") * mouseXSensitivity;
        currentYRotation = Input.GetAxis("Mouse Y") * mouseYSensitivity;

        yRotation -= currentYRotation;
        yRotation = Mathf.Clamp(yRotation, CAM_X_MIN, CAM_X_MAX);

        // rotates camera up and down
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        // rotates player right and left
        playerRef.Rotate(Vector3.up * currentXRotation);
    }

    /*
    private void LateUpdate()
    {
        // rotates camera up and down
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotates player right and left
        playerRef.Rotate(Vector3.up * currentXRotation); 
    }
    */
}
