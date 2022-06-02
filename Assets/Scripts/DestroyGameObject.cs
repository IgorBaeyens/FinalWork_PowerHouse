using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public bool startCountdown = false;
    public int countdown;

    private bool countdownStarted = false;

    private void Update()
    {
        if(startCountdown == true && countdownStarted == false)
        {
            Destroy(gameObject, countdown);
            countdownStarted = true;
        }
    }
}
