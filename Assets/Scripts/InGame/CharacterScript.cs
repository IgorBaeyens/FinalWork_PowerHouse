using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//script in charge of spawning the character that the playerer chose 

public class CharacterScript : MonoBehaviourPunCallbacks
{
    private int characterPhotonViewId;

    public GameObject character;
    private GameObject instantiatedCharacter;
    private GameObject characterPrefabFP;
    private GameObject mainCam;
    private GameObject firstPersonView;

    void Start()
    {
        characterPrefabFP = (GameObject)Resources.Load(character.name + "FP", typeof(GameObject));

        mainCam = gameObject.transform.Find("Main Camera").gameObject;
        firstPersonView = gameObject.transform.Find("First Person View").gameObject;


        if (photonView.IsMine)
        {
            mainCam.SetActive(true);
            firstPersonView.SetActive(true);

            instantiatedCharacter = PhotonNetwork.Instantiate(character.name, gameObject.transform.position, Quaternion.identity);
            characterPhotonViewId = instantiatedCharacter.GetPhotonView().ViewID;
            photonView.RPC("ChangeCharacterParent", RpcTarget.AllBuffered, characterPhotonViewId);

            GameObject instantiatedCharacterFP = Instantiate(characterPrefabFP, transform.position, transform.rotation, firstPersonView.transform);

            foreach (Transform child in instantiatedCharacter.transform)
            {
                child.gameObject.SetActive(false);
            }
        }


        
    }

    //public override void OnJoinedRoom()
    //{
    //    photonView.RPC("ChangeCharacterParent", RpcTarget.All, characterPhotonViewId);
    //}

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    photonView.RPC("ChangeCharacterParent", newPlayer, characterPhotonViewId);
    //}

    [PunRPC] void ChangeCharacterParent(int characterPhotonViewId)
    {
        Debug.Log("fired");
        Debug.Log(characterPhotonViewId);
        Transform character = PhotonView.Find(characterPhotonViewId).transform;
        character.transform.SetParent(photonView.transform);
    }

}
