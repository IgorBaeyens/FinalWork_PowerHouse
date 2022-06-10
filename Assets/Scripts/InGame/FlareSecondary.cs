using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// assign an owner id to your attack
//https://forum.photonengine.com/discussion/16209/bullet-owner

public class FlareSecondary : MonoBehaviourPun
{
    private int playerId;
    private int firstPersonViewId;
    private Quaternion myFPCameraRotation;

    public Transform launchingSpot;
    private Animator launchEffect;
    public GameObject fireBombPrefab;
    private GameObject playerObject;
    private Character flare;
    private int abilityDamage;

    void Start()
    {
        playerObject = transform.parent.parent.gameObject;
        playerId = playerObject.GetPhotonView().ViewID;
        firstPersonViewId = photonView.ViewID;
        launchEffect = launchingSpot.GetComponentInChildren<Animator>();
        flare = GetComponentInParent<CharacterScript>().getCharacter();
        abilityDamage = flare.secondary.damage;
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
            fireBomb.GetComponent<Rigidbody>().AddForce(transform.forward * 34 + transform.up * 0.5f, ForceMode.Impulse);
            photonView.RPC("LogicSecondary", RpcTarget.All, firstPersonViewId, myFPCameraRotation, fireBomb.GetPhotonView().ViewID, playerId, abilityDamage);
            launchEffect.SetTrigger("Fire");
        }
        
    }

    [PunRPC] 
    void LogicSecondary(int firstPersonViewId, Quaternion cameraRotation, int firebombId, int playerId, int abilityDamage)
    {
        GameObject firstPersonCamera = PhotonView.Find(firstPersonViewId).transform.gameObject;
        firstPersonCamera.transform.rotation = cameraRotation;

        GameObject fireBomb = PhotonView.Find(firebombId).gameObject;
        BulletManager bulletManager = fireBomb.GetComponent<BulletManager>();
        bulletManager.setOwnerId(playerId);
        bulletManager.setDamage(abilityDamage);
    }
}
