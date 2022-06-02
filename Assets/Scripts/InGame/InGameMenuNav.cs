using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuNav : MonoBehaviour
{
    private InputActions inputActions;
    private GameObject pauseMenu;
    private bool pressedPause;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        pauseMenu = transform.Find("---Pause Menu---").gameObject;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        pressedPause = inputActions.Player.Pause.triggered;
        
        //unpause the game
        if (pressedPause && GlobalVariables.gamePaused)
        {
            Unpause();
        }
        //pause the game
        else if (pressedPause && !GlobalVariables.gamePaused)
        {
            Pause();
        }
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        GlobalVariables.gamePaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        GlobalVariables.gamePaused = true;
    }
}
