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
    //private int speed = GlobalVariables.selectedCharacter.speed / 10;
    public int speed = 10;
    private float jumpHeight = 8;
    public float moveSpeed;
  
    private InputActions inputActions;
    private GameObject mainCam;
    private GameObject firstPersonView;
    public CharacterController controller;
    private Rigidbody playerRigidbody;

    public Vector2 movementValue;
    public Vector2 lookValue;
    public float lookX;
    public float lookY;
    public bool jumped;
    public bool isGrounded;
    
    private Vector3 velocity;
    private float xAxisRotation = 0;
    private float gravity = 2f;
    private float DistanceToTheGround;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();

        DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        mainCam = gameObject.transform.Find("Main Camera").gameObject;
        firstPersonView = gameObject.transform.Find("First Person View").gameObject;

        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (!GlobalVariables.gamePaused)
            {
                jumped = inputActions.Player.Jump.triggered;
                movementValue = inputActions.Player.Movement.ReadValue<Vector2>();
                lookValue = inputActions.Player.Look.ReadValue<Vector2>();
            }

            //movement
            float moveX = movementValue.x;
            float moveZ = movementValue.y;
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            moveSpeed = new Vector2(moveX, moveZ).magnitude;

            //gravity
            isGrounded = Physics.Raycast(transform.position, -transform.up, DistanceToTheGround + 0.1f);
            if(isGrounded)
            {
                if(jumped)
                    playerRigidbody.AddRelativeForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }

            playerRigidbody.position += move * speed * Time.deltaTime;
            

            //gameObject.transform.position += move * speed * Time.deltaTime;
            //controller.Move(move * speed * Time.deltaTime);
            

            //look
            lookValue *= 0.5f;
            lookValue *= 0.1f;
            lookX = lookValue.x * sensitivity;
            lookY = lookValue.y * sensitivity;

            xAxisRotation += lookY;
            xAxisRotation = Mathf.Clamp(xAxisRotation, -80, 70);

            playerRigidbody.transform.Rotate(Vector3.up * lookX);
            firstPersonView.transform.localRotation = Quaternion.Euler(xAxisRotation, 0, 0);

        }
    }  
}
