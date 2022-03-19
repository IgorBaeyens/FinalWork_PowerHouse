using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    GameObject character;
    public bool useGameObjectModel;

    // Start is called before the first frame update
    void Start()
    {
        character = GlobalVariables.selectedCharacter.model;
        if(useGameObjectModel)
            Instantiate(character, gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
