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
    public int primaryDamage;
    [TextArea(3, 10)]
    public string primaryDescription;
    
    public Ability primary;

    public string secondaryName;
    public Sprite secondaryIcon;
    public int secondaryDamage;
    [TextArea(3, 10)]
    public string secondaryDescription;
    public Ability secondary;

    public string ultimateName;
    public Sprite ultimateIcon;
    public int ultimateDamage;
    public int ultimateDamageOvertime;
    [TextArea(3, 10)]
    public string ultimateDescription;
    public Ability ultimate;

}
