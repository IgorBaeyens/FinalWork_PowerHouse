using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationThirdPerson : MonoBehaviour
{
    private Animator characterAnimator;
    private PlayerMovement movementScript;


    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();

        InvokeRepeating("Blink", 0, 4);
    }

    void Update()
    {
        //run
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed);

        //crouch

        //jump

        //primary

        //secondary

        //ultimate
        
    }

    void Blink()
    {
        characterAnimator.SetTrigger("Blink");
    }
}
