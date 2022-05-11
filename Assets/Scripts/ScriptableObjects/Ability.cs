using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scriptable object for the abilities. this is inherited by the actual ability objects
//and the CastAbility function is overidden so every ability can have it's own logic

public enum Type {primary, secondary, ultimate};

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public Type type;
    public new string name;
    public Sprite icon;
    public int damage;
    [TextArea(5, 10)]
    public string description;
    [TextArea(5, 10)]
    public string updatedDescription;

    
}
