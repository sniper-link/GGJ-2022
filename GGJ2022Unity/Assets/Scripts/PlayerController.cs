using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Camera playerCamera;

    public float movementSpeed = 7f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Update()
    {
        //Vector2 targetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        float forwardMovement = Input.GetAxis("Horizontal");
        float rightMovement = Input.GetAxis("Vertical");

        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;

        Vector3 moveDir = ((playerForward * rightMovement) + (playerRight * forwardMovement)) * movementSpeed;

        characterController.Move(moveDir * Time.deltaTime);
    }
}
