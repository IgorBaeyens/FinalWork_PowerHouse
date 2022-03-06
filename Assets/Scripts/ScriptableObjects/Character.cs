using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
