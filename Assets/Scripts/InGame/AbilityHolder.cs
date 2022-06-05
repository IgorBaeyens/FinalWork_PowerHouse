using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

//this scripts is placed on the player, it contains the ability controls and collects all abilities together in one script

public class AbilityHolder : MonoBehaviour
{
    private InputActions inputActions;
    public bool primaryPressed;
    public bool secondaryPressed;
    public bool ultimatePressed;

    void Start()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Primary.performed += Primary_performed;
        inputActions.Player.Primary.canceled += Primary_canceled;

        inputActions.Player.Secondary.canceled += Secondary_canceled;
        inputActions.Player.Secondary.performed += Secondary_performed;
    }

    private void Secondary_performed(InputAction.CallbackContext context)
    {
        secondaryPressed = true;
    }

    private void Secondary_canceled(InputAction.CallbackContext context)
    {
        secondaryPressed = false;
    }

    private void Primary_performed(InputAction.CallbackContext context)
    {
        primaryPressed = true;
    }

    private void Primary_canceled(InputAction.CallbackContext context)
    {
        primaryPressed = false;
    }
}
