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
    public GameObject beginButton;

    public GameObject playerNamePrefab;
    public GameObject teamOnePlayers;
    private List<Player> teamOne = new List<Player>();
    public GameObject teamTwoPlayers;
    private List<Player> teamTwo = new List<Player>();

    public GameObject rooms;
    public RoomItem lobbyItemPrefab;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public MenuNavigation menuNav;
    private Player host;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        beginButton.SetActive(false);
    }

    //updates the room info in the room itself
    void UpdateRoomInfo()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
                host = player;
        }
        if (PhotonNetwork.IsMasterClient)
            beginButton.SetActive(true);
        else
            beginButton.SetActive(false);
        lobbyTitle.text = PhotonNetwork.CurrentRoom.Name;
        hostName.text = host.NickName;
        gameMode.text = PhotonNetwork.CurrentRoom.CustomProperties["gm"].ToString();
        map.text = PhotonNetwork.CurrentRoom.CustomProperties["map"].ToString();
    }

    void InstantiatePlayerName(Player player)
    {
        GameObject team;

        if (teamOne.Count > teamTwo.Count)
        {
            team = teamTwoPlayers;
            teamTwo.Add(player);
        } else
        {
            team = teamOnePlayers;
            teamOne.Add(player);
        }
        
        GameObject playerNameInstance = Instantiate(playerNamePrefab, team.transform);
        playerNameInstance.GetComponent<PlayerNameItem>().SetUp(player);
    }

    public void beginGame()
    {
        
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

    //when a room is created update its info and go to that room
    public override void OnCreatedRoom()
    {
        menuNav.GoToRoom();
    }

    //when player joins a room
    public override void OnJoinedRoom()
    {
        cachedRoomList.Clear();

        Player[] playerList = PhotonNetwork.PlayerList;
        foreach (Player player in playerList)
        {
            InstantiatePlayerName(player);
        }

        UpdateRoomInfo();
        menuNav.GoToRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        InstantiatePlayerName(newPlayer);
    }

    //TODO: teams should be stored online
    //when you specifically leave the room, go to the rooms menu
    public override void OnLeftRoom()
    {
        menuNav.BackToRooms();
        //teamOne.Clear();
        //teamTwo.Clear();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (teamOne.Contains(otherPlayer))
            teamOne.Remove(otherPlayer);
        else if (teamTwo.Contains(otherPlayer))
            teamTwo.Remove(otherPlayer);
        UpdateRoomInfo();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        GlobalVariables.switchToScene(Scene.mainMenu);
    }

    //When a player leaves or enters a room, or anything at all changes in the roominfo
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
