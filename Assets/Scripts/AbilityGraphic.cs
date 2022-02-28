using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGraphic : MonoBehaviour
{
    

    public string chosenAbilityType;
    private string[] abilityTypes = { "primary", "secondary", "ultimate" };

    public TextMeshProUGUI abilityName;
    public Image abilityIcon;

    //public TextMeshProUGUI primaryName;
    //public Image iconPrimary;
    //public Image iconKeyPrimary;

    //public TextMeshProUGUI secondaryName;
    //public Image iconSecondary;
    //public Image iconKeySecondary;

    //public TextMeshProUGUI ultimateName;
    //public Image iconUltimate;
    //public Image iconKeyUltimate;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Character selectedCharacter = GlobalVariables.selectedCharacter;
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


    }



}
