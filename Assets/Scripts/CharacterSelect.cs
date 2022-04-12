using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//this script contains the character select logic, it stores the scriptable object of the selected character inside a global variable
//as well as updates the ability descriptions with the damage variables

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    public Character selectedCharacter;
    private Ability primary;
    private Ability secondary;
    private Ability ultimate;

    private Toggle toggle;
    public TextMeshProUGUI characterName;
    public Image characterIcon;

    private ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();


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
        if (PhotonNetwork.LocalPlayer.CustomProperties["chara"] != null)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["chara"]);
        }

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
            UpdateDescription(ultimate, "(damage)", ultimate.damage.ToString() + " damage", "(overtimeDamage)", ultimate.overtimeDamage.ToString() + " damage");

            playerProperties["chara"] = GlobalVariables.selectedCharacter.name;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        }
    }

    void UpdateDescription(Ability ability, string damageText, string damage, string overtimeDamageText = "(empty)", string overtimeDamage = "")
    {
        ability.updatedDescription = ability.description.Replace(damageText, damage);
        if (ability.overtimeDamage > 0)
        {
            ability.updatedDescription = ability.updatedDescription.Replace(overtimeDamageText, overtimeDamage);
        }

    }
}
