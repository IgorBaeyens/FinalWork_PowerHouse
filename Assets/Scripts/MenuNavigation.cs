using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

//This script is used for navigating through the different menus stored in the Canvas object

//GRAPHIC RAYCASTER
//https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html

public class MenuNavigation : MonoBehaviour
{
    List<GameObject> menus = new List<GameObject>();

    public GameObject startMenu;
    public GameObject mainMenu;
    public GameObject characterSelectMenu;
    public Animator camAnimator;

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private bool clicked = false;
    private int currentCamState;

    void Start()
    {
        raycaster = gameObject.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
        //gets only the first generation children
        foreach(Transform child in gameObject.transform)
        {
            menus.Add(child.gameObject);
        }

        foreach (GameObject menu in menus)
        {
            if (menu.name == "---Start Menu---")
                menu.SetActive(true);
            else
                menu.SetActive(false);
        }

        currentCamState = camAnimator.GetInteger("States");

        //startMenu.SetActive(true);
        //mainMenu.SetActive(false);
        //characterSelectMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            clicked = true;
        else
            clicked = false;


        //The graphic raycaster for UI elements specifically. sends the UI gameobject the player is hovering over to the hoveredElement global variable
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {       
            GlobalVariables.hoveredElement = result.gameObject;   
        }
        if (results.Count == 0)
            GlobalVariables.hoveredElement = null;




        if(clicked)
        {
            
        }


        if(Input.anyKey)
        {
            changeMenu(startMenu, mainMenu, 1);
            
        }
    
    }

    public void changeMenu(GameObject prevMenu, GameObject newMenu, int camState = -1)
    {
        if(prevMenu.activeSelf)
        {
            prevMenu.SetActive(false);
            newMenu.SetActive(true);
            camAnimator.SetInteger("States", camState);
        }
        
    }

    public void pressedPlay()
    {
        changeMenu(mainMenu, characterSelectMenu);
    }
}
