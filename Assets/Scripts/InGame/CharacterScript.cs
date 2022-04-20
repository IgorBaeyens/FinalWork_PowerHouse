using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//script in charge of spawning the character that the player chose 

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
        //loads a prefab with the selected character name and FP after it in the resource folder
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
            //GameObject instantiatedCharacterFP = Instantiate(characterPrefabFP, transform.position, transform.rotation, transform);


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

}
