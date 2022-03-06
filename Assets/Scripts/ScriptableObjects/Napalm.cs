using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Napalm", menuName = "Napalm")]
public class Napalm : Ability
{
    public override void CastAbility()
    {
        Debug.Log("fires off " + name + " for " + damage + " damage");

    }

}
