using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlareSecondary : MonoBehaviourPun
{
    //public const byte logicSecondaryEventCode = 1;
    private int firstPersonViewId;
    private Quaternion myFPCameraRotation;

    public Transform launchingSpot;
    public GameObject fireBombPrefab;

    void Start()
    {
        firstPersonViewId = photonView.ViewID;
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
        photonView.RPC("LogicSecondary", RpcTarget.All, firstPersonViewId, myFPCameraRotation);
    }

    [PunRPC] 
    void LogicSecondary(int firstPersonViewId, Quaternion cameraRotation)
    {
        GameObject firstPersonCamera = PhotonView.Find(firstPersonViewId).transform.gameObject;
        firstPersonCamera.transform.rotation = cameraRotation;
        GameObject fireBomb = Instantiate(fireBombPrefab, launchingSpot.position, gameObject.transform.parent.parent.rotation);
        Rigidbody fireBombRigidbody = fireBomb.GetComponent<Rigidbody>();
        fireBombRigidbody.AddForce(transform.forward * 34 + transform.up * 0.5f, ForceMode.Impulse);
    }
}
