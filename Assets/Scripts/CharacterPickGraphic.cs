using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterPickGraphic : MonoBehaviour
{
    public Character character;

    public TextMeshProUGUI nameCharacter;
    public Image iconCharacter;


    void Start()
    {
        nameCharacter.text = character.name;
        iconCharacter.sprite = character.iconCharacter;
    }

    void Update()
    {
        

    }
}
