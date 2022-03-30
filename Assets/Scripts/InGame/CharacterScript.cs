using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CharacterScript : MonoBehaviour
{
    private PhotonView photonView;

    private GameObject character;
    private GameObject mainCam;
    private GameObject firstPersonView;
    public bool useGameObjectModel;


    // Start is called before the first frame update
    void Start()
    {
        if (useGameObjectModel) { 
            character = GlobalVariables.selectedCharacter.model;
            Instantiate(character, gameObject.transform);
        }

        mainCam = gameObject.transform.Find("Main Camera").gameObject;
        firstPersonView = gameObject.transform.Find("First Person View").gameObject;

        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            mainCam.SetActive(true);
            firstPersonView.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
