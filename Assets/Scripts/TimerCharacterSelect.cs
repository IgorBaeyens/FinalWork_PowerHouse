using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCharacterSelect : Timer
{
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Event()
    {
        gameManager.LoadLevel("In Game");
    }
}
