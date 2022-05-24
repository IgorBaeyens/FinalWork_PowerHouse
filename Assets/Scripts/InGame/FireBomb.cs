using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireBomb : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (!other.gameObject.GetPhotonView().IsMine)
            //{
                Debug.Log("deal damage");
            //}
        }
    }
}
