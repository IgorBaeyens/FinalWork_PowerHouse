using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBomb : MonoBehaviourPun
{
    public const byte logicSecondaryEventCode = 1;

    public Transform launchingSpot;
    public GameObject fireBombPrefab;

    void Start()
    {
        
    }


    public void CastSecondary()
    {
        photonView.RPC("LogicSecondary", RpcTarget.All);
    }

    [PunRPC] 
    protected virtual void LogicSecondary()
    {
        GameObject fireBomb = Instantiate(fireBombPrefab, launchingSpot.position, Quaternion.identity);
        Rigidbody fireBombRigidbody = fireBomb.GetComponent<Rigidbody>();
        fireBombRigidbody.AddForce((transform.forward * 900 + transform.up * 150));
    }
}
