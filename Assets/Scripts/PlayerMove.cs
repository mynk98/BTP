using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{


    CharacterController controller;

    public float speed;
    float Horizontal;
    float Vertical;
    public Transform Cam;

    public Animator animator;

    [SerializeField] float rotationSmoothTime;
    float currentAngle;
    float currentAngleVelocity;

    [Header("Gravity")]
    float gravity = 9.8f;
    float gravityMultiplier = 2;
    float groundedGravity = -0.5f;
    float jumpHeight = 3f;
    float velocityY = 0;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();

        //HandleGravityAndJump();


    }

    private void HandleMovement()
    {
        //capturing Input from Player
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + Cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);

        }
        else animator.SetBool("isRunning", false);
    }

    void HandleGravityAndJump()
    {
        if (controller.isGrounded && velocityY < 0f) velocityY = groundedGravity;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
            print("jumping");
        }
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }

}