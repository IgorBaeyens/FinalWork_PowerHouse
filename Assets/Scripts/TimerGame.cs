using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGame : Timer
{
    public override void Event()
    {
        Debug.Log("end game");
    }
}
