using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ScoreManager : MonoBehaviourPun
{
    private int blueTeamScore = 0;
    private int redTeamScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncreaseScore(string team)
    {
        if (team == "Blue")
            blueTeamScore++;
        else
            redTeamScore++;
    }
}
