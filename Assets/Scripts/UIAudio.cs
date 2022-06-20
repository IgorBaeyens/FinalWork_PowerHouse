using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    private GameObject hoveredElement;
    private GameObject storedHoveredElement;

    private AudioManager audioManager;

    private bool canPlaySound = true;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        hoveredElement = GlobalVariables.hoveredElement;
        if (hoveredElement != storedHoveredElement)
            canPlaySound = true;
        storedHoveredElement = hoveredElement;

        if (hoveredElement)
        {
            if (hoveredElement.CompareTag("Button") && canPlaySound)
            {
                audioManager.GetSound("hover").source.pitch = Random.Range(2.0f, 2.4f);
                audioManager.Play("hover");
                canPlaySound = false;
            }
        }
        
    }
}
