using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using TMPro;

//player would sometimes be invisible, or extra characters would spawn. This was because i did the code in start() instead of OnLevelFinishedLoading()
//loadlevel was called by everyone aswel, it should only be done by master
//https://answers.unity.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject player;
    private GameObject newPlayer;
    public List<Character> characters = new List<Character>();

    private Transform teamBluePosition;
    private Transform teamRedPosition;
    private List<Transform> respawnPositions = new List<Transform>();

    private Vector3 playerSpawnPoint;
    private Quaternion playerSpawnRotation;
    private int range = 2;

    [PunRPC]
    void LogMessage(string chatMessage)
    {
        GameObject log = GameObject.Find("Log");
        GameObject messageInstance = PhotonNetwork.Instantiate("Message", log.transform.position, Quaternion.identity);
        messageInstance.transform.SetParent(log.transform);
        messageInstance.GetComponent<TMP_Text>().text = chatMessage;
    }

    void Start()
    {
        foreach (Transform child in GameObject.Find("SPAWN_PLAYERS").transform)
        {
            if (child.gameObject.activeSelf)
                respawnPositions.Add(child);
        }

        string chatMessage = $"{newPlayer.GetPhotonView().Owner.NickName} has entered the room";
        photonView.RPC("LogMessage", RpcTarget.All, chatMessage);
    }

    void InitialSpawn()
    {
        teamBluePosition = gameObject.transform.Find("Team Blue").transform;
        teamRedPosition = gameObject.transform.Find("Team Red").transform;
        if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Name == "Blue")
        {
            playerSpawnPoint = RandomizeSpawnPoint(teamBluePosition.position);
            playerSpawnRotation = teamBluePosition.rotation;
        }
        else if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Name == "Red")
        {
            playerSpawnPoint = RandomizeSpawnPoint(teamRedPosition.position);
            playerSpawnRotation = teamRedPosition.rotation;
        }

        newPlayer = PhotonNetwork.Instantiate(player.name, playerSpawnPoint, playerSpawnRotation);

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
        player.transform.position = RandomizeSpawnPoint(respawnPoint.position);
        player.transform.rotation = respawnPoint.rotation;
    }

    Vector3 RandomizeSpawnPoint(Vector3 position)
    {
        float randomX = Random.Range(position.x - range, position.x + range);
        float randomZ = Random.Range(position.z - range, position.z + range);

        Vector3 playerSpawnPoint = new Vector3(randomX, position.y, randomZ);
        return playerSpawnPoint;
    }

    //
    //callbacks
    //

    public override void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    public override void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene arg0, LoadSceneMode arg1)
    {
        InitialSpawn();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }
}
