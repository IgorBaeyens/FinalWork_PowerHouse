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

    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private bool clicked = false;

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

        //startMenu.SetActive(true);
        //mainMenu.SetActive(false);
        //characterSelectMenu.SetActive(false);
    }

    void Update()
    {
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



        if (Input.GetMouseButtonDown(0))
            clicked = true;
        else
            clicked = false;

        if(clicked)
        {
            
        }

        if (startMenu.activeSelf)
        {
            if(Input.anyKey)
            {
                startMenu.SetActive(false);
                mainMenu.SetActive(true);
            }
        }
    }

    public void pressedPlay()
    {
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
    }
}
