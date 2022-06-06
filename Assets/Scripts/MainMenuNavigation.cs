using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

//This script is used for navigating through the different menus stored in the Canvas object

//experimental animation
//https://docs.unity3d.com/ScriptReference/UIElements.Experimental.ValueAnimation_1.html
//GRAPHIC RAYCASTER
//https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html

public class MainMenuNavigation : MonoBehaviour
{
    private List<GameObject> menus = new List<GameObject>();
    private GameObject startMenu, mainMenu;
    private GameObject activeMenu;

    public CanvasGroup fadeCanvas;
    public Animator camAnimator;
    public Animator flareAnimator;

    private GameObject hoveredElement;
    private TMP_InputField nameInput;
    public TMP_Text playerNameText;

    void Start()
    {        
        //gets only the first generation children
        foreach(Transform child in gameObject.transform)
            menus.Add(child.gameObject);

        //sets menu variables
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
            switch (menu.name)
            {
                case "---Start Menu---":
                    startMenu = menu;
                    break;
                case "---Main Menu---":
                    mainMenu = menu;
                    break;
            }
        }
        startMenu.SetActive(true);
        activeMenu = startMenu;
        fadeCanvas.gameObject.SetActive(true);

        nameInput = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (fadeCanvas.alpha != 0)
            fadeCanvas.alpha -= 0.8f * Time.deltaTime;
        else
            fadeCanvas.gameObject.SetActive(false);

        hoveredElement = GlobalVariables.hoveredElement;

        if (hoveredElement)
        {
            switch (hoveredElement.name)
            {
                case "Play":
                    changeState(1);
                    break;
                case "Characters":
                    changeState(2);
                    break;
                case "Options":
                    changeState(3);
                    break;
                case "Quit Game":
                    changeState(4);
                    break;
            }
        }    
    }

    //turns off previous active menu, turns on given menu and set it as active menu. If a camera state is given, change to that state
    void changeMenu(GameObject nextMenu, int camState = -1)
    {
 
        activeMenu.SetActive(false);
        nextMenu.SetActive(true);
        activeMenu = nextMenu;
        if (camState != -1)
             changeState(camState);

    }

    void changeState(int camState)
    {
        camAnimator.SetInteger("States", camState);
        flareAnimator.SetInteger("States", camState);
    }

    public void pressedPlay()
    {
        GlobalVariables.switchToScene(SceneCustom.loading);
    }

    public void pressedEnter()
    {
        if(nameInput.placeholder.GetComponent<TMP_Text>().enabled)
        {
            GlobalVariables.playerName = nameInput.placeholder.GetComponent<TMP_Text>().text;
            changeMenu(mainMenu, 1);
            playerNameText.text = GlobalVariables.playerName;
        } else if (!nameInput.text.Contains(" ") && nameInput.text != "" && nameInput.text.Length < 12)
        {
            GlobalVariables.playerName = nameInput.text;
            changeMenu(mainMenu, 1);
            playerNameText.text = GlobalVariables.playerName;
        }
    }

    public void pressedQuitGame()
    {
        Application.Quit();
    }
}
