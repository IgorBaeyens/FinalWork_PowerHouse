using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class RoomJoin : MonoBehaviourPunCallbacks
{
    public TMP_Text lobbyTitle;
    public TMP_Text hostName;
    public TMP_Text gameMode;
    public TMP_Text map;
    public GameObject beginButton;

    public GameObject playerNamePrefab;
    private GameObject blueTeamMembersObject;
    private GameObject redTeamMembersObject;

    public GameObject rooms;
    public RoomItem lobbyItemPrefab;
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private MenuNavigation menuNav;
    private string host;

    private void Start()
    {
        beginButton.SetActive(false);
        menuNav = FindObjectOfType<MenuNavigation>();

        
    }

    /////////////
    // functions
    /////////////

    void UpdateRoomInfo()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.IsMasterClient)
                host = player.NickName;
        }
        if (PhotonNetwork.IsMasterClient)
            beginButton.SetActive(true);
        else
            beginButton.SetActive(false);
        lobbyTitle.text = PhotonNetwork.CurrentRoom.Name;
        hostName.text = host;
        gameMode.text = PhotonNetwork.CurrentRoom.CustomProperties["gm"].ToString();
        map.text = PhotonNetwork.CurrentRoom.CustomProperties["map"].ToString();
    }
    void AddPlayerName(Player player, PhotonTeam team)
    {
        blueTeamMembersObject = GameObject.Find("Blue Team Members");
        redTeamMembersObject = GameObject.Find("Red Team Members");

        if (team.Name == "Blue")
        {
            GameObject playerNameInstance = Instantiate(playerNamePrefab, blueTeamMembersObject.transform);
            playerNameInstance.GetComponent<PlayerNameItem>().SetUp(player);
        }
        else if (team.Name == "Red")
        {
            GameObject playerNameInstance = Instantiate(playerNamePrefab, redTeamMembersObject.transform);
            playerNameInstance.GetComponent<PlayerNameItem>().SetUp(player);
        }
    }

    /////////////
    // callbacks
    /////////////

    public override void OnJoinedRoom()
    {
        cachedRoomList.Clear();

        menuNav.GoToMenu("---Room---");

        PhotonTeamsManager.PlayerJoinedTeam += OnPlayerJoinedTeam;
        PhotonTeamsManager.PlayerLeftTeam += OnPlayerLeftTeam;

        Player[] playerList = PhotonNetwork.PlayerList;
        foreach (Player player in playerList)
        {
            if (player != PhotonNetwork.LocalPlayer)
                AddPlayerName(player, player.GetPhotonTeam());
        }
        UpdateRoomInfo();
    }
    private void OnPlayerJoinedTeam(Player joinedPlayer, PhotonTeam team)
    {
        AddPlayerName(joinedPlayer, team);
    }
    private void OnPlayerLeftTeam(Player player, PhotonTeam team)
    {
        if (player == PhotonNetwork.LocalPlayer)
        {
            PhotonTeamsManager.PlayerJoinedTeam -= OnPlayerJoinedTeam;
            PhotonTeamsManager.PlayerLeftTeam -= OnPlayerLeftTeam;
        }
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        UpdateRoomInfo();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform child in rooms.transform)
            Destroy(child.gameObject);

        foreach (RoomInfo room in roomList)
        {
            if (!room.RemovedFromList)
                cachedRoomList[room.Name] = room;
            else
                cachedRoomList.Remove(room.Name);
        }

        foreach (KeyValuePair<string, RoomInfo> cachedRoom in cachedRoomList)
        {
            RoomItem newRoomItem = Instantiate(lobbyItemPrefab, rooms.transform);
            newRoomItem.SetRoomInfo(cachedRoom.Value);
        }
    }
}
