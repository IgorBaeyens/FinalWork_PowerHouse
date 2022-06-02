using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    //private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);
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
        PhotonNetwork.LeaveRoom();
    }
    public void disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    /////////////
    // callbacks
    /////////////
    
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.Log(other.NickName + " entered the room");
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log(other.NickName + " left the room");
    }
    public override void OnLeftRoom()
    {
        GlobalVariables.switchToScene(Scene.lobby);
        Destroy(gameObject);
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
