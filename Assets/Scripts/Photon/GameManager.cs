using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    private PhotonTeamsManager teamsManager;
    private int blueTeamMembers;
    private int redTeamMembers;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);

        teamsManager = GetComponent<PhotonTeamsManager>();
    }

    private void Start()
    {
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
        PhotonTeamsManager.PlayerJoinedTeam -= OnPlayerJoinedTeam;
        PhotonTeamsManager.PlayerLeftTeam -= OnPlayerLeftTeam;
        PhotonNetwork.LeaveRoom();
    }
    public void disconnect()
    {
        PhotonNetwork.Disconnect();
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
        GlobalVariables.switchToScene(Scene.lobby);
        Destroy(gameObject);
    }
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log(other.NickName + " entered the room");
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            if (blueTeamMembers > redTeamMembers)
            {
                other.JoinTeam("Red");
            }
            else if (redTeamMembers > blueTeamMembers)
            {
                other.JoinTeam("Blue");
            } else
            {
                other.JoinTeam("Blue");
            }
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log(other.NickName + " left the room");
        
    }
    public void OnPlayerJoinedTeam(Player other, PhotonTeam team)
    {
        blueTeamMembers = teamsManager.GetTeamMembersCount("Blue");
        redTeamMembers = teamsManager.GetTeamMembersCount("Red");
    }
    private void OnPlayerLeftTeam(Player other, PhotonTeam team)
    {
        blueTeamMembers = teamsManager.GetTeamMembersCount("Blue");
        redTeamMembers = teamsManager.GetTeamMembersCount("Red");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        GlobalVariables.switchToScene(Scene.mainMenu);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }
    

}
