using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

//GRAPHIC RAYCASTER
//https://docs.unity3d.com/2017.3/Documentation/ScriptReference/UI.GraphicRaycaster.Raycast.html

public class MenuNavigation : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject mainScreen;
    public GameObject characterSelectScreen;

    private  GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private bool clicked = false;

    void Start()
    {
        raycaster = gameObject.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        
        startScreen.SetActive(true);
        mainScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
    }

    void Update()
    {
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
            
            //Debug.DrawRay(pointerEventData.position, transform.TransformDirection(Vector3.forward) * 10);
        }
        

        if (startScreen.activeSelf)
        {
            if(Input.anyKey)
            {
                startScreen.SetActive(false);
                mainScreen.SetActive(true);
            }
        }
    }

    public void pressedPlay()
    {
        mainScreen.SetActive(false);
        characterSelectScreen.SetActive(true);
    }
}
