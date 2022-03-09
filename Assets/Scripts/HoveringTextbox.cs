using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoveringTextbox : MonoBehaviour
{
    public TextMeshProUGUI textboxContent;
    private CanvasGroup textboxCanvas;
    
    private bool isHoveringOverAbility = false;

    // Start is called before the first frame update
    void Start()
    {
        textboxCanvas = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hoveredElement;

        if (GlobalVariables.hoveredElement != null)
            hoveredElement = GlobalVariables.hoveredElement.transform.parent.gameObject;
        else
            hoveredElement = null;

        Vector3 position = gameObject.transform.position;
        Vector3 nextPosition = Input.mousePosition;
        gameObject.transform.position = Vector3.Slerp(position, nextPosition, 20f * Time.deltaTime);

        if (isHoveringOverAbility)
            textboxCanvas.alpha = 1;
        else
            textboxCanvas.alpha = 0;



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
                    //GlobalVariables.hoveredElement = null;
                    isHoveringOverAbility = false;
                    break;
            }
        }
        else
            isHoveringOverAbility = false;

    }
}
