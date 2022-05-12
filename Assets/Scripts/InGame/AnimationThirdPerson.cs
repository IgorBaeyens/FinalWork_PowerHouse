using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//bug: whole character rotates according to the hip bone
//https://forum.unity.com/threads/why-does-unity-rotate-the-entire-character-according-to-the-root-bone.530199/

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
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed, 0.2f, Time.deltaTime * 4);
        characterAnimator.SetFloat("X", movementScript.movementValue.x, 0.4f, Time.deltaTime * 4);
        characterAnimator.SetFloat("Y", movementScript.movementValue.y, 0.4f, Time.deltaTime * 4);

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

        //secondary

        //ultimate

    }

    void Blink()
    {
        characterAnimator.SetTrigger("Blink");
    }
}
