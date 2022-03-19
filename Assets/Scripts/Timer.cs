using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            timer -= Time.deltaTime;
            timerContent.text = Mathf.Round(timer).ToString();
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

