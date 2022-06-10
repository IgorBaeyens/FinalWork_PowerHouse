using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using Photon.Realtime;

public class PlayerControls : MonoBehaviourPun
{
    private InputActions inputActions;

    public bool pressedPause;
    public bool pressedJump;
    public bool pressedPrimary;
    public bool pressedSecondary;
    public Vector2 movementValue;
    public Vector2 lookValue;

    private bool playerCanPause = true;
    private bool playerCanMove = true;
    private bool playerCanLook = true;
    private bool playerCanShoot = true;

    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Primary.performed += Primary_performed;
        inputActions.Player.Primary.canceled += Primary_canceled;

        inputActions.Player.Secondary.canceled += Secondary_canceled;
        inputActions.Player.Secondary.performed += Secondary_performed;
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (playerCanPause)
            {
                pressedPause = inputActions.Player.Pause.triggered;
            }
            if (playerCanMove)
            {
                pressedJump = inputActions.Player.Jump.triggered;
                movementValue = inputActions.Player.Movement.ReadValue<Vector2>();
            }
            if (playerCanLook)
            {
                lookValue = inputActions.Player.Look.ReadValue<Vector2>();
            }
        }
    }

    public void FreezePlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        SetPlayerCanMove(false);
        SetPlayerCanLook(false);
        SetPlayerCanShoot(false);
    }
    public void UnFreezePlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SetPlayerCanMove(true);
        SetPlayerCanLook(true);
        SetPlayerCanShoot(true);
    }

    //
    // callbacks
    //
   
    private void Primary_performed(InputAction.CallbackContext context)
    {
        if (playerCanShoot)
            pressedPrimary = true;
    }
    private void Primary_canceled(InputAction.CallbackContext context)
    {
        pressedPrimary = false;
    }
    private void Secondary_performed(InputAction.CallbackContext context)
    {
        if (playerCanShoot)
            pressedSecondary = true;
    }
    private void Secondary_canceled(InputAction.CallbackContext context)
    {
        pressedSecondary = false;
    }

    //
    // setters
    //

    public void SetPlayerCanPause(bool set)
    {
        playerCanPause = set;
    }
    public void SetPlayerCanMove(bool set)
    {
        playerCanMove = set;
        movementValue = new Vector2(0, 0);
    }
    public void SetPlayerCanLook(bool set)
    {
        playerCanLook = set;
        lookValue = new Vector2(0, 0);
    }
    public void SetPlayerCanShoot(bool set)
    {
        playerCanShoot = set;
    }
}

