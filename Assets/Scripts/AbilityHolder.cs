using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this scripts is placed on the player, it contains the ability controls and collects all abilities together in one script

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
        }

        if (Input.GetMouseButtonDown(1))
        {
            secondary.CastAbility();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ultimate.CastAbility();
        }
    }
}
