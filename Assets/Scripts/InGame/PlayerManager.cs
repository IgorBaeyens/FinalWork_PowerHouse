using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPun
{
    private GameManager gameManager;

    private string playerName;
    public string playerTeam;
    private Color playerTeamColorRGBA;
    private string playerTeamColorHEX;

    void Start()
    {
        if (photonView.IsMine)
        {
            gameManager = FindObjectOfType<GameManager>();
            playerName = PhotonNetwork.LocalPlayer.NickName;

            photonView.RPC("SetPlayerInfo", RpcTarget.All, photonView.ViewID);
        }
    }

    [PunRPC]
    void SetPlayerInfo(int playerId)
    {
        PhotonView playerPhotonView = PhotonView.Find(playerId);
        Player player = playerPhotonView.Owner;
        PlayerManager playerManager = playerPhotonView.GetComponent<PlayerManager>();
        playerManager.playerName = player.NickName;
        Light teamLight = playerPhotonView.transform.Find("Team Light").GetComponent<Light>();
        gameManager = FindObjectOfType<GameManager>();
        switch (gameManager.GetPlayerTeam(player))
        {
            case "1":
                playerManager.playerTeam = "Blue";
                playerManager.playerTeamColorRGBA = new Color32(4, 130, 255, 255);
                playerManager.playerTeamColorHEX = "0482FF";
                teamLight.color = playerManager.getPlayerTeamColorRGBA();
                break;
            case "2":
                playerManager.playerTeam = "Red";
                playerManager.playerTeamColorRGBA = new Color32(233, 0, 52, 255);
                playerManager.playerTeamColorHEX = "E90048";
                teamLight.color = playerManager.getPlayerTeamColorRGBA();
                break;
        }
    }

    public string getPlayerName()
    {
        return playerName;
    }
    public string getPlayerTeam()
    {
        return playerTeam;
    }
    public Color getPlayerTeamColorRGBA()
    {
        return playerTeamColorRGBA;
    }
    public string getPlayerTeamColorHEX()
    {
        return playerTeamColorHEX;
    }
}
