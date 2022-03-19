using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class UIRaycast : MonoBehaviour
{
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;


    void Start()
    {
        raycaster = gameObject.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }


    void Update()
    {
        //The graphic raycaster for UI elements specifically. sends the UI gameobject the player is hovering over to the hoveredElement global variable
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
            GlobalVariables.hoveredElement = result.gameObject;
        if (results.Count == 0)
            GlobalVariables.hoveredElement = null;
    }
}
