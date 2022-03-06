using UnityEngine;

[CreateAssetMenu(fileName = "Flamethrower", menuName = "Flamethrower")]
public class Flamethrower : Ability
{
    public override void CastAbility()
    {
        Debug.Log("fires off " + name + " for " + damage + " damage");
        
    }

}