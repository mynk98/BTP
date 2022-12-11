using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cakeslice;

public class Player : MonoBehaviour
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

    Camera camera;
    GameObject currentFocussedWaste;
    bool focusFlag;
    GameObject prevFocussedWaste;

    public static GameObject currentlySelected;
    public static int state=0;

    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Cam.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();

        //HandleGravityAndJump();

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "waste")
            {
                currentFocussedWaste = hit.collider.gameObject;
                if (prevFocussedWaste==null) prevFocussedWaste=currentFocussedWaste;
                else
                {
                    prevFocussedWaste.GetComponent<Outline>().eraseRenderer = true;
                    prevFocussedWaste = currentFocussedWaste;
                }
                currentFocussedWaste.GetComponent<Outline>().eraseRenderer=false;
                focusFlag = true;

                if (Input.GetMouseButtonDown(0))
                {
                    currentlySelected = currentFocussedWaste;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    state = 1;
                }
            }

            if (hit.collider.gameObject != currentFocussedWaste && focusFlag)
            {
                currentFocussedWaste.GetComponent<Outline>().eraseRenderer = true;
                focusFlag=false;
            }

            
        }
        
        


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