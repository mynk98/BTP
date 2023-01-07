using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cakeslice;
using UnityEngine.InputSystem;

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
    public static RecycleCheckpoints currentSelectedRecycleCheckpoint;
    public static Dustbin currentSelectedDustbin;

    [Header("UI")]
    [SerializeField] public GameObject binUI;
    [SerializeField] public GameObject binSelectUI;

    public enum PlayerState
    {
        idle,
        collecting,
        sorting,
        driving,
        recycling,
        segregating,
        composting
    };

    public static PlayerState state=PlayerState.idle;

    // Create instance of this 
    private static Player _instance;
    public static Player GetInstance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Cam.GetComponent<Camera>();
        binUI.SetActive(false);
        DeactivateUIHelper();
    }

    // Update is called once per frame
    void Update()
    {

        HandleMovement();
        HandleKeyInput();

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
                    prevFocussedWaste.GetComponent<Waste>().wasteCanvas.gameObject.SetActive(false);
                    prevFocussedWaste = currentFocussedWaste;
                }
                currentFocussedWaste.GetComponent<Outline>().eraseRenderer=false;
                currentFocussedWaste.GetComponent<Waste>().wasteCanvas.gameObject.SetActive(true);
                Transform wasteCanvasTransform = currentFocussedWaste.GetComponent<Waste>().wasteCanvas.transform;
                wasteCanvasTransform.LookAt(wasteCanvasTransform.position+Cam.rotation*Vector3.forward,Cam.rotation*Vector3.up);
                focusFlag = true;

                if (Input.GetMouseButtonDown(0))
                {
                    binUI.SetActive(true);
                    binUI.GetComponentInChildren<TMPro.TMP_Text>().text = "Choose the correct trash bin to put in garbage.";
                    currentlySelected = currentFocussedWaste;
                    ActivateUIHelper();
                    // state = 1;
                    state = PlayerState.collecting;
                    currentFocussedWaste = null;
                }
            }

            else if (hit.collider.gameObject != currentFocussedWaste && focusFlag)
            {
                currentFocussedWaste.GetComponent<Outline>().eraseRenderer = true;
                currentFocussedWaste.GetComponent<Waste>().wasteCanvas.gameObject.SetActive(false);
                focusFlag = false;
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

    private void HandleKeyInput() {
        if (Input.GetKeyDown(KeyCode.B) && state == PlayerState.idle)
        {
            if(state == PlayerState.sorting) {
                print("if ");

                binUI.SetActive(false);
                DeactivateUIHelper();
                // state = 1;
                state = PlayerState.idle;

            }
            else
            {
                print("else ");
                binUI.SetActive(true);
                binUI.GetComponentInChildren<TMPro.TMP_Text>().text = "Choose bin to view its garbage contents.";
                ActivateUIHelper();
                // state = 1;
                state = PlayerState.sorting;
            }
            print("Presed B, State: " +  state);
        }
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

    public static void ActivateUIHelper()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public static void DeactivateUIHelper()
    {
        if (BinSelectUI.GetInstance().gameObject.active) return;
        if (BinSelectUI.GetInstance().binCanvas.active) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void BinUICloseButton()
    {
        if(currentSelectedRecycleCheckpoint!=null)currentSelectedRecycleCheckpoint.isCloseButtonPressed = true;
        SegregationCheckpoint.isCloseButtonPressed = true;
        CompostCheckpoint.isCloseButtonPressed = true;
        binUI.SetActive(false);
        state = PlayerState.idle;
        DeactivateUIHelper();
    }

}