using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ScoreManager : MonoBehaviourPun
{
    private float maxScore = 30;
    private bool gameEnded = false;

    private float blueTeamScore = 0;
    private Image blueScoreBar;
    private TMP_Text blueScoreText;

    private float redTeamScore = 0;
    private Image redScoreBar;
    private TMP_Text redScoreText;

    private GameStateManager gameStateManager;

    void Start()
    {
        blueScoreBar = GameObject.Find("Team Blue Score").GetComponent<Image>();
        redScoreBar = GameObject.Find("Team Red Score").GetComponent<Image>();
        blueScoreText = GameObject.Find("Team Blue Score Number").GetComponent<TMP_Text>();
        redScoreText = GameObject.Find("Team Red Score Number").GetComponent<TMP_Text>();

        blueScoreBar.fillAmount = blueTeamScore;
        redScoreBar.fillAmount = redTeamScore;
        blueScoreText.text = blueTeamScore.ToString();
        redScoreText.text = redTeamScore.ToString();

        gameStateManager = GetComponent<GameStateManager>();
    }

    private void Update()
    {
        if (blueTeamScore >= maxScore && !gameEnded)
        {
            gameStateManager.EndGame("Blue", new Color32(4, 130, 255, 255));
            gameEnded = true;
        }
        else if (redTeamScore >= maxScore && !gameEnded)
        {
            gameStateManager.EndGame("Red", new Color32(233, 0, 52, 255));
            gameEnded = true;
        }
    }

    [PunRPC]
    void UpdateScoreRPC(int matchDataId, float blueTeamScore, float redTeamScore)
    {
        ScoreManager scoreManager = PhotonView.Find(matchDataId).GetComponent<ScoreManager>();
        scoreManager.blueTeamScore = blueTeamScore;
        scoreManager.redTeamScore = redTeamScore;
        scoreManager.UpdateScores(blueTeamScore, redTeamScore);
    }

    public void IncreaseScore(string team)
    {
        if (team == "Blue")
            blueTeamScore += 1;
        else if (team == "Red")
            redTeamScore += 1;
        photonView.RPC("UpdateScoreRPC", RpcTarget.Others, photonView.ViewID, blueTeamScore, redTeamScore);
        UpdateScores(blueTeamScore, redTeamScore);
    }

    private void UpdateScores(float blueTeamScore, float redTeamScore)
    {
        float blueScoreFromOneToZero = blueTeamScore / maxScore;
        blueScoreBar.fillAmount = blueScoreFromOneToZero;
        blueScoreText.text = blueTeamScore.ToString();
        float redScoreFromOneToZero = redTeamScore / maxScore;
        redScoreBar.fillAmount = redScoreFromOneToZero;
        redScoreText.text = redTeamScore.ToString();
    }

    public float GetBlueTeamScore()
    {
        return blueTeamScore;
    }
    public float GetRedTeamScore()
    {
        return redTeamScore;
    }
}
