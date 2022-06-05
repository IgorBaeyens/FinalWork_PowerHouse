using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationFirstPerson : MonoBehaviour
{
    private Animator characterAnimator;
    private PlayerMovement movementScript;
    private AbilityHolder abilityScript;
    private InGameMenuNav inGameMenuNav;
    public Image reticle;

    //LeanTween problem
    //https://stackoverflow.com/questions/60730454/leantween-not-doing-anything-because-of-timescale-0-whats-the-work-around

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        movementScript = GetComponentInParent<PlayerMovement>();
        abilityScript = GetComponentInParent<AbilityHolder>();
        inGameMenuNav = transform.parent.parent.GetComponent<InGameMenuNav>();
        reticle = GameObject.Find("Outer Circle").GetComponent<Image>();
        
    }

    void Update()
    {
        //run
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed);
        
        //roll and lead
        characterAnimator.SetFloat("X", movementScript.lookValue.x, 3f, Time.deltaTime * 4);
        characterAnimator.SetFloat("Y", movementScript.lookValue.y, 3f, Time.deltaTime * 4);

        //jump
        if (movementScript.jumped)
            characterAnimator.SetBool("PressedJump", true);
        else
            characterAnimator.SetBool("PressedJump", false);

        //land
        if (movementScript.isGrounded)
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


        //LTSeq sequence = LeanTween.sequence();
        //sequence.append(LeanTween.scale(reticle.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.05f).setEase(LeanTweenType.easeInElastic));
        //sequence.append(0.2f);
        //sequence.append(LeanTween.scale(reticle.gameObject, new Vector3(1, 1, 1), 0.2f));

}
