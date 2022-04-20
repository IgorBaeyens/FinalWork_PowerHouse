using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFirstPerson : MonoBehaviour
{
    private Animator characterAnimator;
    private PlayerMovement movementScript;

    private Vector2 lookValue;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();

        
    }

    void Update()
    {
        //run
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed);
        
        //
        characterAnimator.SetFloat("X", movementScript.lookValue.x * 2f, 1f, Time.deltaTime * 4);
        characterAnimator.SetFloat("Y", movementScript.lookValue.y * 2f, 1f, Time.deltaTime * 4);

        //crouch

        //jump
        if (movementScript.jumped)
            characterAnimator.SetBool("PressedJump", true);
        else
            characterAnimator.SetBool("PressedJump", false);

        //land
        if (movementScript.controller.isGrounded)
            characterAnimator.SetBool("IsGrounded", true);
        else
            characterAnimator.SetBool("IsGrounded", false);

        //primary

        //secondary

        //ultimate
    }

}
