using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

//https://forum.unity.com/threads/mouse-delta-input.646606/ mouse delta jittering
//https://youtu.be/_QajrabyTJc movement

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;

    public int sensitivity = 10;
    //private int speed = GlobalVariables.selectedCharacter.speed / 10;
    public int speed = 10;
    private float jumpHeight = 1f;
    public float moveSpeed;
  
    private InputActions inputActions;
    private GameObject mainCam;
    private GameObject firstPersonView;
    public CharacterController controller;

    public Vector2 movementValue;
    public Vector2 lookValue;
    public float lookX;
    public float lookY;
    public bool jumped;
    
    private Vector3 velocity;
    private float xAxisRotation = 0;
    private float gravity = 2f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

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
            if (controller.isGrounded)
            {
                velocity.y = -0.1f;
                if(jumped) 
                    velocity.y = jumpHeight;
            } else
                velocity.y -= gravity * Time.deltaTime;          
            move += velocity;


            controller.Move(move * speed * Time.deltaTime);
            Debug.Log(velocity);

            //look
            lookValue *= 0.5f;
            lookValue *= 0.1f;
            lookX = lookValue.x * sensitivity;
            lookY = lookValue.y * sensitivity;

            xAxisRotation += lookY;
            xAxisRotation = Mathf.Clamp(xAxisRotation, -80, 70);

            gameObject.transform.Rotate(Vector3.up * lookX);
            firstPersonView.transform.localRotation = Quaternion.Euler(xAxisRotation, 0, 0);

        }
    }  
}
