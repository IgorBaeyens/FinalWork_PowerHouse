using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomItem : MonoBehaviour
{
    public TMP_Text lobbyName;
    public TMP_Text playerName;
    public TMP_Text gameModeName;
    public TMP_Text mapName;
    public TMP_Text playerCount;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetRoomInfo(RoomInfo room)
    {
        lobbyName.text = room.Name;
        playerName.text = room.CustomProperties["host"].ToString();
        gameModeName.text = room.CustomProperties["gm"].ToString();
        mapName.text = room.CustomProperties["map"].ToString();
        playerCount.text = room.PlayerCount + "/" + room.MaxPlayers;
    }

    public void onClickItem()
    {
        gameManager.JoinRoom(lobbyName.text);
    }
}
