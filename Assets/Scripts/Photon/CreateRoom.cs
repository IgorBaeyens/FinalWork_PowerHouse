using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_Dropdown gameMode;
    public TMP_Dropdown map;
    public TMP_Text playerCountText;

    private int playerCount = 1;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        playerCountText.text = playerCount.ToString();
    }

    void Update()
    {
        
    }

    public void DecreasePlayerCount()
    {
        if (playerCount != 1)
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

    public void CreateTheRoom()
    {
        string roomName;
        if (roomNameInput.placeholder.GetComponent<TMP_Text>().enabled)
            roomName = roomNameInput.placeholder.GetComponent<TMP_Text>().text;
        else
            roomName = roomNameInput.text;
        customProperties["gm"] = gameMode.captionText.text;
        customProperties["map"] = map.captionText.text;
        customProperties["host"] = PhotonNetwork.NickName;
        PhotonNetwork.CreateRoom(roomName, new Photon.Realtime.RoomOptions() { 
            MaxPlayers = (byte)playerCount, 
            CustomRoomProperties = customProperties, 
            CustomRoomPropertiesForLobby = new string[] { "gm", "map", "host" },
            });
    }

}
