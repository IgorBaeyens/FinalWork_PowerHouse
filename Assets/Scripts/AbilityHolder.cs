using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    private Character character;
    private Ability primary;
    private Ability secondary;
    private Ability ultimate;

    void Start()
    {
        character = GlobalVariables.selectedCharacter;
        
        
    }

    void Update()
    {
        character = GlobalVariables.selectedCharacter;
        primary = character.primary;
        secondary = character.secondary;
        ultimate = character.ultimate;

        if (Input.GetMouseButtonDown(0))
        {
            primary.CastAbility();
            //Debug.Log(primary.updatedDescription);
        }

        if (Input.GetMouseButtonDown(1))
        {
            secondary.CastAbility();
            //Debug.Log(secondary.updatedDescription);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ultimate.CastAbility();
            //Debug.Log(ultimate.updatedDescription);
        }
    }
}
