using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float CAM_MAX = 80f;
    const float CAM_MIN = -80f;

    public Transform playerRef;    

    public float mouseXSensitivity = 1.5f;
    public float mouseYSensitivity = 1.0f;

    public float currentXRotation;
    public float currentYRotation;

    float yRotation = 0f;

    static private bool onDialogue = false;

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
        yRotation = Mathf.Clamp(yRotation, CAM_MIN, CAM_MAX);


        // rotates camera up and down
        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
        if (!onDialogue)
        {
            // rotates player right and left
            playerRef.Rotate(Vector3.up * currentXRotation);

        }
    }

    static public void SetOnDialogueTrue()
    {
        onDialogue = true;
    }
    static public void SetOnDialogueFalse()
    {
        onDialogue = false;
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
