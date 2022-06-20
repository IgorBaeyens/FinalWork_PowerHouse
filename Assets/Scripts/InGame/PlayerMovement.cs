using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

//https://answers.unity.com/questions/1777133/laggy-camera-attached-to-rigid-body-player-i-have.html camera jitter with rigidbody
//https://forum.unity.com/threads/mouse-delta-input.646606/ mouse delta jittering
//https://www.unity3dtips.com/unity-fix-movement-stutter/ rigidbody jittering

public class PlayerMovement : MonoBehaviour
{
    private PhotonView photonView;

    public int sensitivity = 10;
    public int speed = 10;
    private float jumpHeight = 8;
    public float moveSpeed;
  
    private InputActions inputActions;
    private HealthManager healthManager;
    private PlayerControls playerControls;
    private GameObject firstPersonView;
    public CharacterController controller;
    private Rigidbody playerRigidbody;

    public Vector3 movement;
    public float lookX;
    public float lookY;
    public bool isGrounded;
    
    private float DistanceToTheGround;

    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        playerRigidbody = GetComponent<Rigidbody>();
        healthManager = GetComponent<HealthManager>();
        DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        firstPersonView = gameObject.transform.Find("First Person View").gameObject;

        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            //movement
            float moveX = playerControls.movementValue.x;
            float moveZ = playerControls.movementValue.y;
            movement = transform.right * moveX + transform.forward * moveZ;
            moveSpeed = new Vector2(moveX, moveZ).magnitude;

            //if (healthManager.currentHealth > 0)
            //{
            //    playerRigidbody.MovePosition(playerRigidbody.position + movement * speed * Time.deltaTime);
            //}

            //jump
            //isGrounded = Physics.Raycast(transform.position, -transform.up, DistanceToTheGround + 0.1f);
            if (isGrounded)
            {
                if (playerControls.pressedJump)
                    playerRigidbody.AddRelativeForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
            }

            //look
            Vector2 lookValue = playerControls.lookValue;
            lookValue *= 0.5f;
            lookValue *= 0.1f;
            lookX = lookValue.x * sensitivity;
            lookY = lookValue.y * sensitivity;


            playerRigidbody.transform.Rotate(Vector3.up * lookX);
            firstPersonView.transform.Rotate(Vector3.right * lookY);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Level"))
            isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Level"))
            isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (healthManager.currentHealth > 0)
        {
            playerRigidbody.MovePosition(playerRigidbody.position + movement * speed * Time.deltaTime);
        }
    }
}
