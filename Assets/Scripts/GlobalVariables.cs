using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script with important variables that are frequently needed, they can be used everywhere

public enum SceneCustom { mainMenu, loading, lobby, characterSelect, inGame };

public class GlobalVariables : MonoBehaviour
{
    //during the character select phase the player can choose their character, and the scriptable object of that character will be stored here.
    public static Character selectedCharacter;

    //whatever UI element the player is hovering over will be stored here.
    public static GameObject hoveredElement;

    public static bool gamePaused = false;

    public static string playerName;

    private GameObject eventSystem;
    private void Start()
    {
        eventSystem = GameObject.Find("EventSystem").gameObject;
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(gameObject);
        selectedCharacter = (Character)Resources.Load("Characters/Flare/Flare", typeof(Character));
    }

    //depending on which scene is selected, load that scene
    public static void switchToScene(SceneCustom scene)
    {
        switch (scene)
        {
            case SceneCustom.mainMenu:
                SceneManager.LoadSceneAsync("Main Menu");
                break;
            case SceneCustom.loading:
                SceneManager.LoadSceneAsync("Loading");
                break;
            case SceneCustom.lobby:
                SceneManager.LoadSceneAsync("Lobby");
                break;
            case SceneCustom.characterSelect:
                SceneManager.LoadSceneAsync("Character Select");
                break;
            case SceneCustom.inGame:
                SceneManager.LoadSceneAsync("In Game");
                break;
        }
    }
}
