using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script with important variables that are frequently needed, they can be used everywhere

public class GlobalVariables : MonoBehaviour
{
    //during the character select phase the player can choose their character, and the scriptable object of that character will be stored here.
    public static Character selectedCharacter;

    //whatever UI element the player is hovering over will be stored here.
    public static GameObject hoveredElement;


}
