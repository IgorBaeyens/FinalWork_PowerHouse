using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerGame : Timer
{
    private ScoreManager scoreManager;
    private GameStateManager gameStateManager;

    protected override void Start()
    {
        base.Start();
        scoreManager = FindObjectOfType<ScoreManager>();
        gameStateManager = FindObjectOfType<GameStateManager>();
    }

    public override void Event()
    {
        if (scoreManager.GetBlueTeamScore() > scoreManager.GetRedTeamScore())
        {
            gameStateManager.EndGame("Blue", new Color32(4, 130, 255, 255));
        } else if (scoreManager.GetBlueTeamScore() < scoreManager.GetRedTeamScore())
        {
            gameStateManager.EndGame("Red", new Color32(233, 0, 52, 255));
        } else if (scoreManager.GetBlueTeamScore() == scoreManager.GetRedTeamScore())
        {
            gameStateManager.EndGame("Draw", new Color32(255, 255, 255, 255));
        }
    }
}
