using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CharacterScript : MonoBehaviourPunCallbacks
{
    private int characterPhotonViewId;

    public GameObject character;
    private GameObject instantiatedCharacter;
    private GameObject mainCam;
    private GameObject firstPersonView;
    public bool useGameObjectModel;

    void Start()
    {
        mainCam = gameObject.transform.Find("Main Camera").gameObject;
        firstPersonView = gameObject.transform.Find("First Person View").gameObject;

        //photonViews.Add(photonView);
        
        if (photonView.IsMine)
        {
            mainCam.SetActive(true);
            firstPersonView.SetActive(true);

            instantiatedCharacter = PhotonNetwork.Instantiate(character.name, gameObject.transform.position, Quaternion.identity);
            characterPhotonViewId = instantiatedCharacter.GetPhotonView().ViewID;
            photonView.RPC("ChangeParent", RpcTarget.All, characterPhotonViewId);
        } 
    }

    [PunRPC] void ChangeParent(int characterPhotonViewId)
    {

        Transform character = PhotonView.Find(characterPhotonViewId).transform;
        character.transform.SetParent(photonView.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
