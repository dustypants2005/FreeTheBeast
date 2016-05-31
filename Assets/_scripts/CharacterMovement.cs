using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

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

    private CharacterController _playerController;

    public float rotateSensitivity = 1;
    public float deadzone = .01f;
    public bool amIRightStick = true;
    private Vector3 playerDirection;

    public GameObject characterModel;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    /// <summary>
    /// Update the player's controls in the Update()
    /// </summary>
    /// <param name="isRooted"></param>
    /// <param name="isPlayerInControl"></param>
    public void UpdateMovement(bool isRooted, bool isPlayerInControl)
    {
        if(!isPlayerInControl) return; // if we are not in control, we should not all the player to control their own character.
        if (!isRooted) // if we are rooted we should not be able to move. We shouldn't bother calculating movement if we are not planning to move 
        {
            CalculatePlayerMovement();
            MovePlayer();
        }
        RotatePlayer();
    }

	
    public void Init(bool isLocalPlayer, CharacterController controller)
    {
        FindCamera();
        if (camera == null)
        {
            spawnCamera();
        }

        if (isLocalPlayer)
        {
            _playerController = controller;
        }

        SetInputs();
    }
       
    /// <summary>
    /// Update the Camera's position and rotation in a FixedUpdate().
    /// </summary>
    public void FixedUpdateForCamera()
    {
        MoveCamera();           
    } 

    void MovePlayer()
    {
        _playerController.Move(moveDirection * Time.deltaTime);
    }

    void CalculatePlayerMovement()
    {

        if (_playerController.isGrounded) // only move if grounded
        {
            moveDirection = new Vector3(lh, 0, lv);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
    }

    /// <summary>
    /// We are rotate the player either with the left stick or the right stick.
    /// </summary>
    void RotatePlayer()
    {
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

            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
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
        var newPosition = transform.position + new Vector3(0, cameraDistance, 0);
        camera.transform.position = Vector3.Slerp(camera.transform.position, newPosition, cameraFollowSpeed);
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
