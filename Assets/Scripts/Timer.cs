using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//timer
//https://answers.unity.com/questions/45676/making-a-timer-0000-minutes-and-seconds.html

public class Timer : MonoBehaviour
{
    public float timer;
    public bool timerFinished = false;

    private TextMeshProUGUI timerContent;

    void Start()
    {
        timerContent = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(timer >= 0)
        {
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            timer -= Time.deltaTime;
            timerContent.text = niceTime;
        } else
        {
            if(!timerFinished)
            {
                timerFinished = true;
                Event();
            }
        }
    }

    public virtual void Event()
    {
        Debug.Log("play event");
    }
}

