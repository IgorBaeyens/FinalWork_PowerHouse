using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuNav : MonoBehaviour
{
    private InputActions inputActions;
    private bool pressedPause;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Enable();
    }

    void Update()
    {
        pressedPause = inputActions.Player.Pause.triggered;
        
        //unpause the game
        if (pressedPause && GlobalVariables.gamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GlobalVariables.gamePaused = false;
        }
        //pause the game
        else if (pressedPause && !GlobalVariables.gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
            GlobalVariables.gamePaused = true;
        }
    }
}
