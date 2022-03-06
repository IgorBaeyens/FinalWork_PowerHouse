using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireBomb", menuName = "Fire Bomb")]
public class FireBomb : Ability
{
    public override void CastAbility()
    {
        Debug.Log("fires off " + name + " for " + damage + " damage");

    }

}
