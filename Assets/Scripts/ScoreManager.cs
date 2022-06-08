using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ScoreManager : MonoBehaviourPun
{
    private float maxScore = 5;

    private float blueTeamScore = 0;
    private bool blueVictory = false;
    private Image blueScoreBar;
    private TMP_Text blueScoreText;

    private float redTeamScore = 0;
    private bool redVictory = false;
    private Image redScoreBar;
    private TMP_Text redScoreText;


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
    }

    private void Update()
    {
        if (blueTeamScore >= maxScore)
            blueVictory = true;
        else if (redTeamScore >= maxScore)
            redVictory = true;
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
}
