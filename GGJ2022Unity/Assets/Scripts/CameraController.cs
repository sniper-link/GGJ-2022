using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float CAM_Y_MAX = 50f;
    const float CAM_Y_MIN = -50f;

    public Transform playerRef;    

    public float mouseXSensitivity = 1.5f;
    public float mouseYSensitivity = 1.0f;

    public float currentXRotation;
    public float currentYRotation;

    private void Awake()
    {
        currentXRotation = 0;
        currentYRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentXRotation += (Input.GetAxis("Mouse X") * mouseXSensitivity);
        currentYRotation -= (Input.GetAxis("Mouse Y") * mouseYSensitivity);

        currentYRotation = Mathf.Clamp(currentYRotation, CAM_Y_MIN, CAM_Y_MAX);
    }

    private void LateUpdate()
    {
        // rotates camera up and down
        transform.localRotation = Quaternion.Euler(currentYRotation, 0, 0);
        // rotates player right and left
        playerRef.rotation = Quaternion.Euler(0, currentXRotation, 0);
    }
}
