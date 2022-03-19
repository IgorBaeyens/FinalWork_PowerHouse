using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCharacterSelect : Timer
{
    public override void Event()
    {
        GlobalVariables.switchToScene(Scene.inGame);
    }
}
