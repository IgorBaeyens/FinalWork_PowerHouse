using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGraphic : MonoBehaviour
{
    
    private Character character;
    private Ability primary;
    private Ability secondary;
    private Ability ultimate;

    public Type type;

    public TextMeshProUGUI abilityName;
    public Image abilityIcon;

    private void Start()
    {
        character = GlobalVariables.selectedCharacter;
        
    }

    void Update()
    {
        character = GlobalVariables.selectedCharacter;
        primary = character.primary;
        secondary = character.secondary;
        ultimate = character.ultimate;

        switch (type)
        {
            case Type.primary:
                abilityName.text = primary.name;
                abilityIcon.sprite = primary.icon;
                break;
            case Type.secondary:
                abilityName.text = secondary.name;
                abilityIcon.sprite = secondary.icon;
                break;
            case Type.ultimate:
                abilityName.text = ultimate.name;
                abilityIcon.sprite = ultimate.icon;
                break;
        }

    }

}
