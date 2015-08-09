using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public float speed = 1.0f; // CHARACTER SPEED
    public float playerFollowSpeed = 1.0f;
    public GameObject camera;
    public float cameraFollowSpeed = 1.0f;
    public float cameraDistance = -10.0f;
    public Quaternion cameraRotation = new Quaternion(90, 0, 0, 90);

    private float lh; // LEFT HORIZONTAL
    private float lv; // LEFT VERTICAL
    private float rh; // RIGHT HORIZONTAL
    private float rv; // RIGHT VERTICAL

    public float rotateSensitivity = 1;
    public float deadzone = .01f;
    public bool amIRightStick = true;
    private Vector3 playerDirection;

    public GameObject characterModel;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {        
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime); 
        SetInputs();
        CalculatePlayerMovement();
        RotatePlayer();
    }

	
    void Start()
    {
        FindCamera();
        if (camera == null)
        {
            spawnCamera();
        }
    }
       
    void FixedUpdate()
    {
        MoveCamera();           
    } 

    void MovePlayer(CharacterController controller)
    {
        controller.Move((-playerDirection) * Time.deltaTime);
    }

    void CalculatePlayerMovement()
    {
       playerDirection += (Vector3.right * lh * deadzone +
          Vector3.forward * lv * deadzone).normalized * speed;

       if (playerDirection.sqrMagnitude > 1f)
       {
           playerDirection = playerDirection.normalized;
       }
    }

    /// <summary>
    /// We are rotate the player either with the left stick or the right stick.
    /// </summary>
    void RotatePlayer()
    {
        // tried to write this as "float angle, right, left = 0;" but unity kept giving errors on only "right"
        float angle = 0;
        float right = 0;
        float left = 0;

        if(IsRightStickUsed())
        {
            right = Mathf.Atan2(rh, -rv) * Mathf.Rad2Deg;
        }
        if (IsLeftStickUsed())
        {
            left = Mathf.Atan2(lh, lv) * Mathf.Rad2Deg;
        }

        if (right != 0 || left != 0)
        {
            angle = left;
            if (IsRightStickUsed())
            {
                angle = right;
            }

            characterModel.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
        
    }

    bool IsRightStickUsed()
    {
        if(rh > deadzone || rh < -deadzone || rv > deadzone || rv < -deadzone)
        {
            return true;
        }

        return false;
    }

    bool IsLeftStickUsed()
    {
        if (lh > deadzone || lh < -deadzone || lv > deadzone || lv < -deadzone)
        {
            return true;
        }

        return false;
    }

    void spawnCamera()
    {
        var camera = new Camera();
        Instantiate(camera);
    }

    void FindCamera()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void MoveCamera()
    {
        camera.transform.position = transform.position + new Vector3(0 , cameraDistance, 0); 
        camera.transform.rotation = cameraRotation;
    }

    void SetInputs()
    {
        if (Input.GetAxis("RightStickHorizontal") >= deadzone || Input.GetAxis("RightStickHorizontal") <= deadzone)
        {
            rh = Input.GetAxis("RightStickHorizontal");
        }

        if (Input.GetAxis("RightStickVertical") >= deadzone || Input.GetAxis("RightStickVertical") <= deadzone)
        {
            rv = Input.GetAxis("RightStickVertical");
        }

        if (Input.GetAxis("Horizontal") >= deadzone || Input.GetAxis("Horizontal") <= deadzone)
        {
            lh = Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") >= deadzone || Input.GetAxis("Vertical") <= deadzone)
        {
            lv = Input.GetAxis("Vertical");
        }
    }
}
