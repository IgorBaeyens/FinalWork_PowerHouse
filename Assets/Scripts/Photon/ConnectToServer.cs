using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.NickName = GlobalVariables.playerName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        GlobalVariables.switchToScene(SceneCustom.lobby);
    }

    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    Debug.Log("COULD NOT CONNECT - OFFLINE MODE STARTED");
    //    PhotonNetwork.OfflineMode = true;
    //}
}
