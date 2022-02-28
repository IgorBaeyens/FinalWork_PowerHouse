using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "character")]
public class Character : ScriptableObject
{

    public string characterName;
    public int speed;
    public int health;

    public GameObject character;

    public Sprite characterIcon;

    public string primaryName;
    public Sprite primaryIcon;

    public string secondaryName;
    public Sprite secondaryIcon;

    public string ultimateName;
    public Sprite ultimateIcon;

}
