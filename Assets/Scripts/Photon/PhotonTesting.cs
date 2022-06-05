using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonTesting : MonoBehaviourPunCallbacks
{

    public GameObject player;

    private Vector3 teamAPosition;
    private int range = 2;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN.");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {



        //Debug.Log("joined" + PhotonNetwork.CurrentRoom);

        //teamAPosition = gameObject.transform.Find("Team A").transform.position;

        //float randomX = Random.Range(teamAPosition.x - range, teamAPosition.x + range);
        //float randomZ = Random.Range(teamAPosition.z - range, teamAPosition.z + range);

        //Vector3 teamASpawnpoint = new Vector3(randomX, teamAPosition.y, randomZ);

        //GameObject newPlayer = PhotonNetwork.Instantiate(player.name, teamASpawnpoint, Quaternion.identity);
    }

  
}
