using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Camera playerCamera;
    public float playerMoveSpeed = 10f;

    public float movementSpeed = 7f;
    // might not need
    public float gravity = 20f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = playerCamera.transform.forward;
        Vector3 camRight = playerCamera.transform.right;

        Vector3 moveDir = new Vector3(((camForward.x * vertical) + (camRight.x * horizontal)) * movementSpeed, 0, ((camForward.z * vertical) + (camRight.z * horizontal)) * movementSpeed);
        moveDir.y -= gravity;
        characterController.Move(moveDir * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
    }
}
