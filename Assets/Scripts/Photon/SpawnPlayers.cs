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

        float randomX = Random.Range(teamAPosition.x - range, teamAPosition.x + range);
        float randomZ = Random.Range(teamAPosition.z - range, teamAPosition.z + range);

        Vector3 teamASpawnpoint = new Vector3(randomX, teamAPosition.y, randomZ);

        PhotonNetwork.Instantiate(player.name, teamASpawnpoint, Quaternion.identity);
    }


}
