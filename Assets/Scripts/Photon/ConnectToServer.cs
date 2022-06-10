using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    // Release builds cannot connect to the same region as the unity editor, the unity editor and development builds have their own region called "development region"

    void Start()
    {
        PhotonNetwork.NickName = GlobalVariables.playerName;
        PhotonNetwork.ConnectUsingSettings();
        GlobalVariables.switchToScene(SceneCustom.lobby);
    }

}
