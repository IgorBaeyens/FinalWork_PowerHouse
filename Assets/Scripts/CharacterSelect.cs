using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Character selectedCharacter;
    private Ability primary;
    private Ability secondary;
    private Ability ultimate;

    private Toggle toggle;
    public TextMeshProUGUI characterName;
    public Image characterIcon;


    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        if (toggle.isOn)
        {
            GlobalVariables.selectedCharacter = selectedCharacter;
        }

        if (selectedCharacter)
        {
            characterName.text = selectedCharacter.name;
            characterIcon.sprite = selectedCharacter.icon;
        }

    }

    void Update()
    {
        

    }

    public void changeToCharacter()
    {
        if (toggle.isOn)
        {
            GlobalVariables.selectedCharacter = selectedCharacter;
            primary = GlobalVariables.selectedCharacter.primary;
            secondary = GlobalVariables.selectedCharacter.secondary;
            ultimate = GlobalVariables.selectedCharacter.ultimate;


            UpdateDescription(primary, "(damage)", primary.damage.ToString() + " damage");
            UpdateDescription(secondary, "(damage)", secondary.damage.ToString() + " damage");
            UpdateDescription(ultimate, "(damage)", ultimate.damage.ToString() + " damage");
            UpdateDescription(ultimate, "(overtimeDamage)", ultimate.overtimeDamage.ToString() + " damage");

            Debug.Log(GlobalVariables.selectedCharacter );
        }
    }

    void UpdateDescription(Ability ability, string oldText, string newText)
    {
        ability.updatedDescription = ability.description.Replace(oldText, newText);

    }
}
