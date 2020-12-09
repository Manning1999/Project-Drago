using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HurtableObject
{


    //create singleton
    public static PlayerController instance;
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerController>();
            }

            return _instance;
        }
    }


    CharacterController characterController = null;

    [SerializeField]
    private GameObject camera = null;

    [SerializeField]
    private float mouseSensitivity = 0.2f;

    [SerializeField]
    protected float minLookAngle, maxLookAngle;

    bool canControlLooking = true;

    float walkingSpeed = 3;
    float runningSpeed = 7;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;


    [SerializeField]
    Vector2 rotation = Vector2.zero;

    bool canMove = true;

    Rigidbody rb = null;

    bool isRunning = false;

    int jumpForce = 200;

                
    public void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        characterController = transform.GetComponent<CharacterController>();
    }

    public void Update()
    {

        Look();
        Move();
    }


    protected void Look()
    {
        if (canControlLooking == true)
        {
            

            rotation.x += Input.GetAxis("Mouse X");
            rotation.y += Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation.x * mouseSensitivity, 0));

            if (rotation.y > maxLookAngle) rotation.y = maxLookAngle;
            if (rotation.y < minLookAngle) rotation.y = minLookAngle;

            camera.transform.localRotation = Quaternion.Euler(new Vector3(-rotation.y * mouseSensitivity, 0, 0));
           
        }
    }


    protected bool GetGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.up), out hit, 1f))
        {
            Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, -transform.TransformDirection(Vector3.up) * 1f, Color.white);
            return false;
        }
    }

    protected void Move()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        
    }



    public void SetCanLook(bool set)
    {
        canControlLooking = set;
    }

    
   

}
