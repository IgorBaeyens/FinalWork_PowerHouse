using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_Text lobbyTitle;
    public TMP_Text hostName;
    public TMP_Text gameMode;
    public TMP_Text map;

    public GameObject teamOnePlayers;
    public GameObject teamTwoPlayers;
    public GameObject playerNamePrefab;

    public GameObject rooms;
    public RoomItem lobbyItemPrefab;
    //private List<RoomItem> roomItemsList = new List<RoomItem>();
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public MenuNavigation menuNav;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    //Change the room content depending on info given when the room was created
    public override void OnCreatedRoom()
    {
        lobbyTitle.text = PhotonNetwork.CurrentRoom.Name;
        hostName.text = PhotonNetwork.NickName;
        gameMode.text = PhotonNetwork.CurrentRoom.CustomProperties["gm"].ToString();
        map.text = PhotonNetwork.CurrentRoom.CustomProperties["map"].ToString();
        menuNav.GoToRoom();
    }

    //What happens when a player joins the room
    public override void OnJoinedRoom()
    {
        cachedRoomList.Clear();
        GameObject playerNameInstance = Instantiate(playerNamePrefab, teamOnePlayers.transform);
        playerNameInstance.GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
        menuNav.GoToRoom();
    }

    //What happens when a player leaves the room
    public override void OnLeftRoom()
    {
        menuNav.BackToRooms();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        GlobalVariables.switchToScene(Scene.mainMenu);
    }

    //What happens when a change occurs to any room in the lobby
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (Transform child in rooms.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList)
            {
                cachedRoomList[room.Name] = room;
            } else
            {
                cachedRoomList.Remove(room.Name);
            }
        }

        foreach (KeyValuePair<string, RoomInfo> cachedRoom in cachedRoomList)
        {
            RoomItem newRoomItem = Instantiate(lobbyItemPrefab, rooms.transform);
            newRoomItem.SetRoomInfo(cachedRoom.Value);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}
