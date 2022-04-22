using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//script with important variables that are frequently needed, they can be used everywhere

public enum Scene { mainMenu, loading, lobby, characterSelect, inGame };

public class GlobalVariables : MonoBehaviour
{
    //during the character select phase the player can choose their character, and the scriptable object of that character will be stored here.
    public static Character selectedCharacter;

    //whatever UI element the player is hovering over will be stored here.
    public static GameObject hoveredElement;

    public static bool gamePaused = false;

    //depending on which scene is selected, load that scene
    public static void switchToScene(Scene scene)
    {
        switch (scene)
        {
            case Scene.mainMenu:
                SceneManager.LoadSceneAsync("Main Menu");
                break;
            case Scene.loading:
                SceneManager.LoadSceneAsync("Loading");
                break;
            case Scene.lobby:
                SceneManager.LoadSceneAsync("Lobby");
                break;
            case Scene.characterSelect:
                SceneManager.LoadSceneAsync("Character Select");
                break;
            case Scene.inGame:
                SceneManager.LoadSceneAsync("In Game");
                break;
        }
    }

    public static string playerName;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        selectedCharacter = (Character)Resources.Load("Characters/Flare/Flare", typeof(Character));
    }

}
