using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpforce;
    public float airmultiplier;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground")]
    public float PlayerHeight;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.3f;
    public Transform groundCheckPoint;
    private bool isGrounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundMask);

        rb.drag = isGrounded ? groundDrag : 0;

        // Debugging
        // Debug.Log($"Is Grounded: {isGrounded}");
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Grounded movement
        if (isGrounded)
        {
            rb.velocity = new Vector3(
                moveDirection.normalized.x * moveSpeed,
                rb.velocity.y,
                moveDirection.normalized.z * moveSpeed
            );
        }
        // Airborne movement
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset Y velocity to ensure consistent jump height
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
    }
}