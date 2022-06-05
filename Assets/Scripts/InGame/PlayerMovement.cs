using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

//https://answers.unity.com/questions/1777133/laggy-camera-attached-to-rigid-body-player-i-have.html camera jitter with rigidbody
//https://forum.unity.com/threads/mouse-delta-input.646606/ mouse delta jittering
//https://youtu.be/_QajrabyTJc movement

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;

    public int sensitivity = 10;
    public int speed = 10;
    private float jumpHeight = 8;
    public float moveSpeed;
  
    private InputActions inputActions;
    private GameObject firstPersonView;
    public CharacterController controller;
    private InGameMenuNav ingameMenuNav;
    private Rigidbody playerRigidbody;

    public Vector2 movementValue;
    public Vector2 lookValue;
    public float lookX;
    public float lookY;
    public bool jumped;
    public bool isGrounded;
    private bool playerCanMove = true;
    private bool playerCanLook = true;
    private bool pressedPause;
    
    private float DistanceToTheGround;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        firstPersonView = gameObject.transform.Find("First Person View").gameObject;
        //ingameMenuNav = GameObject.Find("Canvas").GetComponent<InGameMenuNav>();

        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (playerCanMove)
            {
                jumped = inputActions.Player.Jump.triggered;
                movementValue = inputActions.Player.Movement.ReadValue<Vector2>();
            }
            if (playerCanLook)
            {
                lookValue = inputActions.Player.Look.ReadValue<Vector2>();
            }

            //movement
            float moveX = movementValue.x;
            float moveZ = movementValue.y;
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            moveSpeed = new Vector2(moveX, moveZ).magnitude;

            //jump
            isGrounded = Physics.Raycast(transform.position, -transform.up, DistanceToTheGround + 0.1f);
            if(isGrounded)
            {
                if(jumped)
                    playerRigidbody.AddRelativeForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }

            playerRigidbody.position += move * speed * Time.deltaTime;


            //look
            lookValue *= 0.5f;
            lookValue *= 0.1f;
            lookX = lookValue.x * sensitivity;
            lookY = lookValue.y * sensitivity;


            playerRigidbody.transform.Rotate(Vector3.up * lookX);
            firstPersonView.transform.Rotate(Vector3.right * lookY);
        }
    }  

    public void SetPlayerCanMove(bool onOrOff)
    {
        playerCanMove = onOrOff;
    }
    public void SetPlayerCanLook(bool onOrOff)
    {
        playerCanLook = onOrOff;
    }
}
