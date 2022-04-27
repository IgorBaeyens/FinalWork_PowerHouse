using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "FireBomb", menuName = "Fire Bomb")]
public class FireBomb : Ability
{
    

    public override void CastAbility()
    {
        Debug.Log("fires off " + name + " for " + damage + " damage");


        //Transform launchingSpot = flare.transform.Find("DEF-f_middle.03.R");
        //Debug.Log(launchingSpot.position);
        //PhotonNetwork.pla
        //PhotonNetwork.Instantiate("Fire Bomb", )
    }

}
