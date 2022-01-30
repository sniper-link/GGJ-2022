using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public Camera playerCamera;

    public float movementSpeed = 7f;

    private Vector3 currentInputVector;
    private Vector3 smoothInputVelocity;
    [SerializeField]
    private float smoothInputSpeed = .05f;

    /*
    // If we need a jump function -> uncomment this and the Jump code below
    // Jump variable
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundmask;

    Vector3 velocity;
    bool isGrounded;
    */
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Vector2 targetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        float forwardMovement = Input.GetAxis("Vertical");
        float rightMovement = Input.GetAxis("Horizontal");
        
        Vector3 playerForward = transform.forward;
        Vector3 playerRight = transform.right;

        Vector3 targetInput = playerForward * forwardMovement + playerRight * rightMovement;

        currentInputVector = Vector3.SmoothDamp(currentInputVector, targetInput, ref smoothInputVelocity, smoothInputSpeed);

        Vector3 moveDir = new Vector3(currentInputVector.x, 0, currentInputVector.z);

        characterController.Move(movementSpeed * Time.deltaTime * moveDir);

        /*
        // Jump code
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundmask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
        */
    }
}
