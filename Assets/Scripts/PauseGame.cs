using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private PlayerControls playerControls;
    private ChangeMenu changeMenu;

    private bool gamePaused;

    private void Start()
    {
        changeMenu = GetComponent<ChangeMenu>();
        Invoke("GetPlayerControls", 2f);
    }

    private void Update()
    {
        if (playerControls)
        {
            if (playerControls.pressedPause && gamePaused)
            {
                Debug.Log("game should unpause");
                changeMenu.m_DisableMenu("---Pause Menu---");
                m_UnPause();
            }
            else if (playerControls.pressedPause && !gamePaused)
            {
                Debug.Log("game should pasue");
                changeMenu.m_ActivateMenu("---Pause Menu---");
                m_Pause();
            }
        }  
    }

    public void m_Pause()
    {
        playerControls.FreezePlayer();
        gamePaused = true;
    }
    public void m_UnPause()
    {
        playerControls.UnFreezePlayer();
        gamePaused = false;
    }

    private void GetPlayerControls()
    {
        playerControls = GameObject.Find("Local Player").GetComponent<PlayerControls>();
    }
}
