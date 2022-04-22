using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFirstPerson : MonoBehaviour
{
    private Animator characterAnimator;
    private PlayerMovement movementScript;
    private AbilityHolder abilityScript;
    


    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();
        abilityScript = GetComponentInParent<AbilityHolder>();

        
    }

    void Update()
    {
        //run
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed);
        
        //roll and lead
        characterAnimator.SetFloat("X", movementScript.lookValue.x * 2f, 1f, Time.deltaTime * 4);
        characterAnimator.SetFloat("Y", movementScript.lookValue.y * 2f, 1f, Time.deltaTime * 4);

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
        if (abilityScript.primaryPressed)
            characterAnimator.SetBool("IsPrimaryOn", true);
        else
            characterAnimator.SetBool("IsPrimaryOn", false);

        //secondary
        if (abilityScript.secondaryPressed)
            characterAnimator.SetBool("IsSecondaryOn", true);
        else
            characterAnimator.SetBool("IsSecondaryOn", false);

        //ultimate

        
    }

    public void CastPrimary()
    {
        abilityScript.CastPrimary();
    }

    public void CastSecondary()
    {
        abilityScript.CastSecondary();
    }
}
