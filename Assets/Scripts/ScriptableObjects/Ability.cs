using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {primary, secondary, ultimate};

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public Type type;
    public new string name;
    public Sprite icon;
    public int damage;
    public int overtimeDamage;
    [TextArea(5, 10)]
    public string description;
    [TextArea(5, 10)]
    public string updatedDescription;


    public virtual void CastAbility() { }
    
}
