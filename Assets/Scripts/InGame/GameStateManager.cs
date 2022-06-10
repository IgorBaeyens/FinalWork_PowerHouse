using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

//find local player game object
//https://answers.unity.com/questions/1128578/how-do-i-find-the-player-gameobject-of-the-local-p.html

public class GameStateManager : MonoBehaviourPun
{
    public GameObject endGameScreen;

    public TMP_Text teamText;
    public TMP_Text winText;

    void Start()
    {
        endGameScreen.SetActive(false);
    }

    public void EndGame(string winningTeam, Color teamColor)
    {
        GameObject player = GameObject.Find("Local Player");
        PlayerControls playerControls = player.GetComponent<PlayerControls>();

        teamText.text = winningTeam;
        teamText.color = teamColor;
        if (winningTeam == "Blue")
        {
            teamText.fontSize = 90;
            winText.fontSize = 68;
        } else if (winningTeam == "Red")
        {
            teamText.fontSize = 100;
            winText.fontSize = 60;
        } else
        {
            winText.text = "";
        }

        endGameScreen.SetActive(true);
        playerControls.FreezePlayer();
        playerControls.SetPlayerCanPause(false);
    }
}
