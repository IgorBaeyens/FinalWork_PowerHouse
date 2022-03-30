using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
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
        characterAnimator.SetFloat("Speed", movementScript.moveSpeed);
        if (movementScript.movementValue.y < 0) characterAnimator.SetBool("Backwards", true);
        else characterAnimator.SetBool("Backwards", false);

        
        
    }

    void Blink()
    {
        characterAnimator.SetTrigger("Blink");
    }
}
