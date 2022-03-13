using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    private bool gameStarted = false;

    private TextMeshProUGUI timerContent;

    void Start()
    {
        timerContent = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
            timerContent.text = Mathf.Round(timer).ToString();
        } else
        {
            if(!gameStarted)
            {
                gameStarted = true;
                Debug.Log("start game");
            }
        }
    }
}
