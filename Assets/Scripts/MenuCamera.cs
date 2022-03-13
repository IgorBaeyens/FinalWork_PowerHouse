using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private Animator camAnimator;

    void Start()
    {
        camAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }
}
