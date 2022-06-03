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

    private Vector3 teamBluePosition;
    private Vector3 teamRedPosition;
    private Vector3 playerSpawnPoint;
    private int range = 2;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        teamBluePosition = gameObject.transform.Find("Team Blue").transform.position;
        teamRedPosition = gameObject.transform.Find("Team Red").transform.position;

        /////////////////
        // initial spawn
        /////////////////

        if (gameManager.GetPlayerTeam(PhotonNetwork.LocalPlayer) == "1")
        {
            playerSpawnPoint = GetSpawnPoint(teamBluePosition);
        } else if (gameManager.GetPlayerTeam(PhotonNetwork.LocalPlayer) == "2")
        {
            playerSpawnPoint = GetSpawnPoint(teamRedPosition);
        }

        GameObject newPlayer = PhotonNetwork.Instantiate(player.name, playerSpawnPoint, Quaternion.identity);
        
        foreach (Character character in characters)
        {
            if (character.name == PhotonNetwork.LocalPlayer.CustomProperties["chara"].ToString())
            {
                newPlayer.GetComponent<CharacterScript>().character = character.model;
            }
        }
    }

    Vector3 GetSpawnPoint(Vector3 teamPosition)
    {
        float randomX = Random.Range(teamPosition.x - range, teamPosition.x + range);
        float randomZ = Random.Range(teamPosition.z - range, teamPosition.z + range);

        Vector3 playerSpawnPoint = new Vector3(randomX, teamPosition.y, randomZ);
        return playerSpawnPoint;
    }

}
