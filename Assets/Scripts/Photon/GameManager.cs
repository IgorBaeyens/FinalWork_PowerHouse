using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private GameObject eventSystem;
    private GameObject globalVars;

    private PhotonTeamsManager teamsManager;
    private int blueTeamMembersCount;
    private int redTeamMembersCount;
    public Player[] blueTeamMembers;
    public Player[] redTeamMembers;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);

        teamsManager = GetComponent<PhotonTeamsManager>();
        
    }

    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").gameObject;
        globalVars = GameObject.Find("GLOBAL_VARIABLES").gameObject;
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
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }
    public void Disconnect()
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
        PhotonTeamsManager.PlayerJoinedTeam += OnPlayerJoinedTeam;
        PhotonTeamsManager.PlayerLeftTeam += OnPlayerLeftTeam;

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
            PhotonNetwork.LocalPlayer.JoinTeam("Blue");
    }
    public override void OnLeftRoom()
    {
        GlobalVariables.switchToScene(SceneCustom.loading);
        Destroy(gameObject);

    }
    public override void OnLeftLobby()
    {
        Disconnect();
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
        
        if (other == PhotonNetwork.LocalPlayer)
        {
            PhotonTeamsManager.PlayerJoinedTeam -= OnPlayerJoinedTeam;
            PhotonTeamsManager.PlayerLeftTeam -= OnPlayerLeftTeam;
            Debug.Log("gameManager: actions have been deleted");
        }
        UpdateTeams();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        GlobalVariables.switchToScene(SceneCustom.mainMenu);
        Destroy(eventSystem);
        Destroy(globalVars);
        Destroy(gameObject);
    }
}
