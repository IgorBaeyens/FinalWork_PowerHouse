using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomCreate : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_Dropdown gameMode;
    public TMP_Dropdown map;
    public TMP_Text playerCountText;

    private MenuNavigation menuNav;
    private int playerCount = 2;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        menuNav = FindObjectOfType<MenuNavigation>();
        playerCountText.text = playerCount.ToString();
    }

    public void DecreasePlayerCount()
    {
        if (playerCount != 2)
        {
            playerCount--;
            playerCountText.text = playerCount.ToString();
        }
    }
    public void IncreasePlayerCount()
    {
        if (playerCount != 10)
        {
            playerCount++;
            playerCountText.text = playerCount.ToString();
        }
    }

    //everytime this function is called create a room with roomname, max players and custom properties
    public void CreateRoom()
    {
        string roomName;
        if (roomNameInput.placeholder.GetComponent<TMP_Text>().enabled)
            roomName = roomNameInput.placeholder.GetComponent<TMP_Text>().text;
        else
            roomName = roomNameInput.text;
        customProperties["gm"] = gameMode.captionText.text;
        customProperties["map"] = map.captionText.text;
        customProperties["host"] = PhotonNetwork.NickName;
        customProperties["inGame"] = false;
        PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions() { 
            MaxPlayers = (byte)playerCount, 
            CustomRoomProperties = customProperties, 
            CustomRoomPropertiesForLobby = new string[] { "gm", "map", "host", "inGame" },
            });
    }

    public override void OnCreatedRoom()
    {
        menuNav.GoToMenu("---Room---");
    }

}
