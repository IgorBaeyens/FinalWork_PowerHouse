using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    private CharacterScript characterScript;
    public List<Character> characters = new List<Character>();

    private Vector3 teamAPosition;
    private int range = 2;

    void Start()
    {
        teamAPosition = gameObject.transform.Find("Team A").transform.position;

        float randomX = Random.Range(teamAPosition.x - range, teamAPosition.x + range);
        float randomZ = Random.Range(teamAPosition.z - range, teamAPosition.z + range);

        Vector3 teamASpawnpoint = new Vector3(randomX, teamAPosition.y, randomZ);

        GameObject newPlayer = PhotonNetwork.Instantiate(player.name, teamASpawnpoint, Quaternion.identity);

        foreach (Character character in characters)
        {
            if (character.name == PhotonNetwork.LocalPlayer.CustomProperties["chara"].ToString())
            {
                newPlayer.GetComponent<CharacterScript>().character = character.model;
            }
        }
    }


}
