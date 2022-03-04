using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPickGraphic : MonoBehaviour
{
    public Character pickedCharacter;

    private Toggle toggle;
    public TextMeshProUGUI characterName;
    public Image characterIcon;


    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        if (toggle.isOn)
        {
            GlobalVariables.selectedCharacter = pickedCharacter;
        }

        if (pickedCharacter)
        {
            characterName.text = pickedCharacter.characterName;
            characterIcon.sprite = pickedCharacter.characterIcon;
        }

    }

    void Update()
    {
        

    }

    public void changeToCharacter()
    {
        Debug.Log(GlobalVariables.selectedCharacter );
        if (toggle.isOn)
        {
            GlobalVariables.selectedCharacter = pickedCharacter;
            
        }
    }
}
