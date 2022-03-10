using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//script for the text box that follows the mouse around

public class HoveringTextbox : MonoBehaviour
{
    public TextMeshProUGUI textboxContent;
    private CanvasGroup textboxCanvas;
    
    private bool isHoveringOverAbility = false;

    void Start()
    {
        //canvas group is an easy way to make UI elements transparant
        textboxCanvas = gameObject.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        //shorter version of "GlobalVariables.hoveredElement.gameObject"
        GameObject hoveredElement;
        if (GlobalVariables.hoveredElement != null)
            hoveredElement = GlobalVariables.hoveredElement.gameObject;
        else
            hoveredElement = null;

        //makes the textbox follow the mouse with easing applied
        Vector3 position = gameObject.transform.position;
        Vector3 nextPosition = Input.mousePosition;
        gameObject.transform.position = Vector3.Slerp(position, nextPosition, 20f * Time.deltaTime);

        //makes the texbox visible or invisible depending on what it's hovering over
        if (isHoveringOverAbility)
            textboxCanvas.alpha = Mathf.Lerp(textboxCanvas.alpha, 1, 20f * Time.deltaTime);
        else
            textboxCanvas.alpha = Mathf.Lerp(textboxCanvas.alpha, 0, 20f * Time.deltaTime);

        if (hoveredElement != null)
        {
            switch (hoveredElement.name)
            {
                case "Primary":
                    textboxContent.text = GlobalVariables.selectedCharacter.primary.updatedDescription;
                    isHoveringOverAbility = true;
                    break;
                case "Secondary":
                    textboxContent.text = GlobalVariables.selectedCharacter.secondary.updatedDescription;
                    isHoveringOverAbility = true;
                    break;
                case "Ultimate":
                    textboxContent.text = GlobalVariables.selectedCharacter.ultimate.updatedDescription;
                    isHoveringOverAbility = true;
                    break;
                default:
                    isHoveringOverAbility = false;
                    break;
            }
        }
        else
            isHoveringOverAbility = false;

    }
}
