using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// assign an owner id to your attack
//https://forum.photonengine.com/discussion/16209/bullet-owner

public class FlareSecondary : MonoBehaviourPun
{
    private int playerId;
    private string playerTeam;
    private int firstPersonViewId;
    private Quaternion myFPCameraRotation;

    public Transform launchingSpot;
    private Animator launchEffect;
    public GameObject fireBombPrefab;
    private GameObject playerObject;

    void Start()
    {
        playerObject = transform.parent.parent.gameObject;
        playerId = playerObject.GetPhotonView().ViewID;
        //playerTeam = playerObject.GetComponent<PlayerManager>().getPlayerTeam();
        firstPersonViewId = photonView.ViewID;
        launchEffect = launchingSpot.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            myFPCameraRotation = transform.parent.rotation;
        }
    }

    public void CastSecondary()
    {
        if (photonView.IsMine)
        {
            GameObject fireBomb = PhotonNetwork.Instantiate("Fire Bomb", launchingSpot.position, gameObject.transform.parent.parent.rotation);
            playerTeam = playerObject.GetComponent<PlayerManager>().getPlayerTeam();
            photonView.RPC("LogicSecondary", RpcTarget.All, firstPersonViewId, myFPCameraRotation, fireBomb.GetPhotonView().ViewID, playerTeam);
            launchEffect.SetTrigger("Fire");
        }
        
    }

    [PunRPC] 
    void LogicSecondary(int firstPersonViewId, Quaternion cameraRotation, int firebombId, string playerTeam)
    {
        GameObject firstPersonCamera = PhotonView.Find(firstPersonViewId).transform.gameObject;
        firstPersonCamera.transform.rotation = cameraRotation;

        GameObject fireBomb = PhotonView.Find(firebombId).gameObject;
        BulletManager bulletManager = fireBomb.GetComponent<BulletManager>();
        bulletManager.setOwnerId(playerId);
        bulletManager.setOwnerTeam(playerTeam);
        Rigidbody fireBombRigidbody = fireBomb.GetComponent<Rigidbody>();
        fireBombRigidbody.AddForce(transform.forward * 34 + transform.up * 0.5f, ForceMode.Impulse);
    }
}
