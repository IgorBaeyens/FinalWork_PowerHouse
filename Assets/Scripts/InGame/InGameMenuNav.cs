using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuNav : MonoBehaviour
{
    private InputActions inputActions;
    private bool pressedPause;

    private List<GameObject> menus = new List<GameObject>();
    private GameObject gameHUD, pauseMenu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        //gets only the first generation children
        foreach (Transform child in gameObject.transform)
            menus.Add(child.gameObject);

        //sets menu variables
        foreach (GameObject menu in menus)
        {
            switch (menu.name)
            {
                case "---Game HUD---":
                    gameHUD = menu;
                    break;
                case "---Pause Menu---":
                    pauseMenu = menu;
                    menu.SetActive(false);
                    break;
            }
        }

    }

    void Update()
    {
        pressedPause = inputActions.Player.Pause.triggered;
        
        //unpause the game
        if (pressedPause && GlobalVariables.gamePaused)
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GlobalVariables.gamePaused = false;
        }
        //pause the game
        else if (pressedPause && !GlobalVariables.gamePaused)
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GlobalVariables.gamePaused = true;
        }
    }
}
