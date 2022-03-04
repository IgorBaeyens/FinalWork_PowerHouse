using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGraphic : MonoBehaviour
{
    Character selectedCharacter;

    private string primaryDescriptionComplete;
    private string secondaryDescriptionComplete;
    private string ultimateDescriptionComplete;

    public string chosenAbilityType;
    private string[] abilityTypes = { "primary", "secondary", "ultimate" };

    public TextMeshProUGUI abilityName;
    public Image abilityIcon;

    private void Start()
    {
        selectedCharacter = GlobalVariables.selectedCharacter;
        primaryDescriptionComplete = selectedCharacter.primaryDescription.Replace("(damage)", selectedCharacter.primaryDamage.ToString());
        secondaryDescriptionComplete = selectedCharacter.primaryDescription.Replace("(damage)", selectedCharacter.primaryDamage.ToString());
        //completeDescription(primaryDescriptionComplete, selectedCharacter.primaryDescription, "(damage)", selectedCharacter.primaryDamage.ToString());
    }

    void Update()
    {
        selectedCharacter = GlobalVariables.selectedCharacter;
        if (abilityTypes[0] == chosenAbilityType)
        {
            abilityName.text = selectedCharacter.primaryName;
            abilityIcon.sprite = selectedCharacter.primaryIcon;
        }
        else if (abilityTypes[1] == chosenAbilityType)
        {
            abilityName.text = selectedCharacter.secondaryName;
            abilityIcon.sprite = selectedCharacter.secondaryIcon;
        }
        else if (abilityTypes[2] == chosenAbilityType)
        {
            abilityName.text = selectedCharacter.ultimateName;
            abilityIcon.sprite = selectedCharacter.ultimateIcon;
        }

        Debug.Log(primaryDescriptionComplete);
    }

    //void completeDescription(string descriptionComplete, string description, string willBeReplaced, string willReplace)
    //{
    //    descriptionComplete = description.Replace(willBeReplaced, willReplace);
    //}
}
