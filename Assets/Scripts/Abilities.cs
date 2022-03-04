using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : Ability
{
    public override void CastAbility()
    {
        if(name == "Flare")
        {
            switch (type)
            {
                case Type.primary:
                    Flamethrower();
                    break;
                case Type.secondary:
                    break;
            }

        }
    }

    void Flamethrower()
    {
        Debug.Log("fires off flamethrower for " + damage + " damage");
    }
}
