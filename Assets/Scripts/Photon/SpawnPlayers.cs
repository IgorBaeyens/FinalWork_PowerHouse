using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    private Vector3 teamAPosition;
    private int range = 2;

    void Start()
    {
        teamAPosition = gameObject.transform.Find("Team A").transform.position;

        Vector2 teamASpawnpoint = new Vector2(Random.Range(teamAPosition.x - range, teamAPosition.x + range), Random.Range(teamAPosition.z - range, teamAPosition.z + range));

        PhotonNetwork.Instantiate(player.name, teamASpawnpoint, Quaternion.identity);
    }


}
