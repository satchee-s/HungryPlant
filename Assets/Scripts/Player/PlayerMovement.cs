using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;
    float finalSpeed;
    [SerializeField] float gravity = -9.81f * 2;
    [SerializeField] float jumpHeight;

    public Transform groundCheck;
    [SerializeField] float groundDistance;
    public LayerMask groundLayerMask;

    float crouchHeight = 0.5f;
    float normalHeight = 1f;

    Vector3 velocity;
    bool isGrounded;
    bool canJump;

    private void Update()
    {
        Sprint();
        Movement();
        Jump();
        Crouch();
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        controller.Move(move * finalSpeed * Time.deltaTime);
    }
    private void Crouch()
    {
        Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        canJump = true;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            newScale.y = crouchHeight;
            canJump = false;
        }
        transform.localScale = newScale;
    }

    private void Sprint()
    {
        finalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            finalSpeed = sprintSpeed;
        }
    }
}
