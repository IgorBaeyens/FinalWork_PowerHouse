using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//script in charge of spawning the character that the player chose 

public class CharacterScript : MonoBehaviourPunCallbacks
{
    private int characterPhotonViewId;
    private int characterPhotonViewIdFP;

    public GameObject character;
    private GameObject characterPrefabFP;
    private GameObject instantiatedCharacter;
    private GameObject instantiatedCharacterFP;
    private GameObject mainCam;
    private GameObject firstPersonView;

    void Start()
    {
        mainCam = gameObject.transform.Find("Main Camera").gameObject;
        firstPersonView = gameObject.transform.Find("First Person View").gameObject;


        if (photonView.IsMine)
        {
            mainCam.SetActive(true);
            firstPersonView.SetActive(true);

            instantiatedCharacter = PhotonNetwork.Instantiate(character.name, gameObject.transform.position + new Vector3(0, -0.05f, 0), Quaternion.identity);
            characterPhotonViewId = instantiatedCharacter.GetPhotonView().ViewID;
            photonView.RPC("ChangeCharacterParent", RpcTarget.AllBuffered, characterPhotonViewId);

            instantiatedCharacterFP = PhotonNetwork.Instantiate(character.name + "FP", gameObject.transform.position, Quaternion.identity);
            characterPhotonViewIdFP = instantiatedCharacterFP.GetPhotonView().ViewID;
            photonView.RPC("ChangeCharacterFPParent", RpcTarget.AllBuffered, characterPhotonViewIdFP);

            foreach (Transform child in instantiatedCharacter.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

    }

    [PunRPC] void ChangeCharacterParent(int characterPhotonViewId)
    {
        Transform character = PhotonView.Find(characterPhotonViewId).transform;
        character.transform.SetParent(photonView.transform);
    }

    [PunRPC]
    void ChangeCharacterFPParent(int characterPhotonViewId)
    {
        Transform character = PhotonView.Find(characterPhotonViewId).transform;
        character.transform.SetParent(photonView.transform.Find("First Person View").transform);
    }
}
