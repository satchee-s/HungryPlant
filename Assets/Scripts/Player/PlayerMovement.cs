using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchSpeed;
    float finalSpeed;
    [SerializeField] float gravity = -9.81f * 2;

    public Transform groundCheck;
    [SerializeField] float groundDistance;

    public Animator playerAnimator;

    float crouchHeight = 0.5f;
    float normalHeight = 1f;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private void Update()
    {
        Sprint();
        Movement();
        Crouch();
        FlashBang();
        velocity.y += gravity *Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Movement()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * finalSpeed * Time.deltaTime);
        isMoving = true;
    }
    private void Crouch()
    {
        finalSpeed = speed;
        Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            newScale.y = crouchHeight;
            finalSpeed = crouchSpeed;
            playerAnimator.SetBool("Crouch", true);
        }
        else
        {
            playerAnimator.SetBool("Crouch", false);
        }
        
        transform.localScale = newScale;
    }

    private void Sprint()
    {
        finalSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift) && isMoving)
        {
            finalSpeed = sprintSpeed;
            playerAnimator.SetBool("Running", true);
        }
        else
        {
            playerAnimator.SetBool("Running", false);
        }
    }

    public IEnumerator Flash()
    {
        playerAnimator.SetBool("Flash", true);
        yield return new WaitForSeconds(.5f);
        playerAnimator.SetBool("Flash", false);
    }

    public void FlashBang()
    {
        StartCoroutine(Flash());
    }
}
