using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private PhotonTeamsManager teamsManager;
    private int blueTeamMembersCount;
    private int redTeamMembersCount;
    public Player[] blueTeamMembers;
    public Player[] redTeamMembers;

    private MenuNavigation menuNav;
    Scene currentScene;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);

        teamsManager = GetComponent<PhotonTeamsManager>();
        menuNav = FindObjectOfType<MenuNavigation>();
    }

    private void Start()
    {
        PhotonTeamsManager.PlayerJoinedTeam -= OnPlayerJoinedTeam;
        PhotonTeamsManager.PlayerLeftTeam -= OnPlayerLeftTeam;
        PhotonTeamsManager.PlayerJoinedTeam += OnPlayerJoinedTeam;
        PhotonTeamsManager.PlayerLeftTeam += OnPlayerLeftTeam;
    }

    /////////////
    // functions
    /////////////

    public void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LocalPlayer.LeaveCurrentTeam();
        PhotonNetwork.LeaveRoom();
    }
    public void disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    private void UpdateTeams()
    {
        blueTeamMembersCount = teamsManager.GetTeamMembersCount("Blue");
        teamsManager.TryGetTeamMembers("Blue", out blueTeamMembers);
        redTeamMembersCount = teamsManager.GetTeamMembersCount("Red");
        teamsManager.TryGetTeamMembers("Red", out redTeamMembers);
    }
    public string GetPlayerTeam(Player player)
    {
        return player.CustomProperties["_pt"].ToString();
    }

    /////////////
    // callbacks
    /////////////

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
            PhotonNetwork.LocalPlayer.JoinTeam("Blue");
    }
    public override void OnLeftRoom()
    {
        currentScene = SceneManager.GetActiveScene();
        if(currentScene.name != "Lobby")
        {
            GlobalVariables.switchToScene(SceneCustom.lobby);
            Destroy(gameObject);
        } else
        {
            //GlobalVariables.switchToScene(SceneCustom.loading);
            menuNav.GoToMenu("---Rooms---");
        }
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log(other.NickName + " entered the room");
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            string teamColor;

            if (blueTeamMembersCount > redTeamMembersCount)
                teamColor = "Red";
            else if (redTeamMembersCount > blueTeamMembersCount)
                teamColor = "Blue";
            else
                teamColor = "Blue";

            other.JoinTeam(teamColor);
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log(other.NickName + " left the room");
    }
    public void OnPlayerJoinedTeam(Player other, PhotonTeam team)
    {
        UpdateTeams();
    }
    private void OnPlayerLeftTeam(Player other, PhotonTeam team)
    {
        UpdateTeams();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Destroy(GameObject.Find("EventSystem").gameObject);
        GlobalVariables.switchToScene(SceneCustom.mainMenu);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }


}
