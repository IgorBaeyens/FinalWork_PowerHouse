using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuNav : MonoBehaviour
{
    private InputActions inputActions;
    private GameObject pauseMenu;
    private PlayerMovement playerMovement;
    private AbilityHolder playerAbilities;
    private bool pressedPause;
    public bool gamePaused;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputActions = new InputActions();
        inputActions.Player.Enable();

        pauseMenu = GameObject.Find("Canvas").transform.Find("---Pause Menu---").gameObject;
        playerMovement = GetComponent<PlayerMovement>();
        playerAbilities = GetComponent<AbilityHolder>();
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        pressedPause = inputActions.Player.Pause.triggered;
        
        if (pressedPause && gamePaused)
        {
            Unpause();
        }
        else if (pressedPause && !gamePaused)
        {
            Pause();
        }
    }

    public void Unpause()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
        playerMovement.SetPlayerCanMove(true);
        playerMovement.SetPlayerCanLook(true);
        playerAbilities.SetPlayerCanShoot(true);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        gamePaused = true;
        playerMovement.SetPlayerCanMove(false);
        playerMovement.SetPlayerCanLook(false);
        playerAbilities.SetPlayerCanShoot(false);
    }
}
