using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float CAM_Y_MAX = -50f;
    const float CAM_Y_MIN = 20f;

    public Transform playerRef;
    public Transform cameraTarget;
    public bool thirdPersonCamera;
    public float cameraDistance;

    public float currentXRotation;
    public float currentYRotation;

    private void Awake()
    {
        currentXRotation = 0;
        currentYRotation = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentXRotation += (Input.GetAxis("Mouse X") * 1.5f);
        currentYRotation -= (Input.GetAxis("Mouse Y") * 1.0f);

        currentYRotation = Mathf.Clamp(currentYRotation, CAM_Y_MAX, CAM_Y_MIN);
    }

    private void LateUpdate()
    {
        //Vector3 dir = new Vector3(0, 0, -cameraDistance);
        //Quaternion rotation = Quaternion.Euler(currentYRotation, currentXRotation, 0);
        //transform.position = cameraTarget.position + rotation * dir * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentYRotation, currentXRotation, 0);
        //transform.LookAt(cameraTarget.position);
        //playerCamTrans.position = playerTrans.position + rotation * dir;
        //playerCamTrans.LookAt(playerTrans.position);
        //playerRef.rotation = Quaternion.Euler(0, currentXRotation, 0);
    }
}
