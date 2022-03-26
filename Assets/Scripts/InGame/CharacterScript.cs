using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private GameObject character;
    private GameObject mainCamera;
    public bool useGameObjectModel;

    private AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {
        character = GlobalVariables.selectedCharacter.model;
        if(useGameObjectModel)
            Instantiate(character, gameObject.transform);

        mainCamera = transform.Find("Main Camera").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
