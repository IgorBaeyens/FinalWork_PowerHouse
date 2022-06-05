using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public List<Character> characters = new List<Character>();

    private Transform teamBluePosition;
    private Transform teamRedPosition;
    private List<Transform> respawnPositions = new List<Transform>();

    private Vector3 playerSpawnPoint;
    private Quaternion playerSpawnRotation;
    private int range = 2;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        foreach(Transform child in GameObject.Find("SPAWN_PLAYERS").transform)
        {
            respawnPositions.Add(child);
        }

        initialSpawn();
    }

    void initialSpawn()
    {
        teamBluePosition = gameObject.transform.Find("Team Blue").transform;
        teamRedPosition = gameObject.transform.Find("Team Red").transform;

        if (gameManager.GetPlayerTeam(PhotonNetwork.LocalPlayer) == "1")
        {
            playerSpawnPoint = RandomizeSpawnPoint(teamBluePosition.position);
            playerSpawnRotation = teamBluePosition.rotation;
        }
        else if (gameManager.GetPlayerTeam(PhotonNetwork.LocalPlayer) == "2")
        {
            playerSpawnPoint = RandomizeSpawnPoint(teamRedPosition.position);
            playerSpawnRotation = teamRedPosition.rotation;
        }

        GameObject newPlayer = PhotonNetwork.Instantiate(player.name, playerSpawnPoint, playerSpawnRotation);

        foreach (Character character in characters)
        {
            if (character.name == PhotonNetwork.LocalPlayer.CustomProperties["chara"].ToString())
            {
                newPlayer.GetComponent<CharacterScript>().character = character;
            }
        }
    }

    public void Respawn(GameObject player)
    {
        int randomIndex = Random.Range(0, respawnPositions.Count);
        Transform respawnPoint = respawnPositions[randomIndex];
        player.transform.position = respawnPoint.position;
        player.transform.rotation = respawnPoint.rotation;
    }

    Vector3 RandomizeSpawnPoint(Vector3 teamPosition)
    {
        float randomX = Random.Range(teamPosition.x - range, teamPosition.x + range);
        float randomZ = Random.Range(teamPosition.z - range, teamPosition.z + range);

        Vector3 playerSpawnPoint = new Vector3(randomX, teamPosition.y, randomZ);
        return playerSpawnPoint;
    }

}
