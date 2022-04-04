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

    private CreateAndJoinRoom roomScript;

    private void Start()
    {
        roomScript = FindObjectOfType<CreateAndJoinRoom>();
    }

    //public void SetRoomInfo(string phLobbyName, string phPlayerName, string phGameModeName, string phMapName, string currentPlayers, string maxPlayers)
    //{
    //    lobbyName.text = phLobbyName;
    //    playerName.text = phPlayerName;
    //    gameModeName.text = phGameModeName;
    //    mapName.text = phMapName;
    //    playerCount.text = currentPlayers + "/" + maxPlayers;
    //}

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
        roomScript.JoinRoom(lobbyName.text);
    }
}
