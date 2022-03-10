using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptable object for the characters, it takes the model and ability scriptable objects

[CreateAssetMenu(fileName = "New Character", menuName = "character")]
public class Character : ScriptableObject
{
    public new string name;
    public int speed;
    public int health;

    public GameObject model;
    public Sprite icon;

    public Ability primary;
    public Ability secondary;
    public Ability ultimate;

}
