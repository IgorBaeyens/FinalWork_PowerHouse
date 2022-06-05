using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPun
{
    private GameManager gameManager;

    public string playerTeam;

    void Start()
    {
        if (photonView.IsMine)
        {
            gameManager = FindObjectOfType<GameManager>();

            photonView.RPC("SetPlayerTeam", RpcTarget.All, photonView.ViewID);
        }
    }

    [PunRPC]
    void SetPlayerTeam(int playerId)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerId);
        Player player = playerPhotonView.Owner;
        PlayerManager playerManager = playerPhotonView.GetComponent<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
        switch (gameManager.GetPlayerTeam(player))
        {
            case "1":
                playerManager.playerTeam = "Blue";
                break;
            case "2":
                playerManager.playerTeam = "Red";
                break;
        }
    }

    public string getPlayerTeam()
    {
        return playerTeam;
    }
}
