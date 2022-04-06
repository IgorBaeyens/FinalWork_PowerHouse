using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerNameItem : MonoBehaviourPunCallbacks
{

    private Player player;
    public TMP_Text playerName;

    public void SetUp(Player givenPlayer)
    {
        player = givenPlayer;
        playerName.text = givenPlayer.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(otherPlayer == player)
            Destroy(gameObject);
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
