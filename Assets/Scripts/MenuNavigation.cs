using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject mainScreen;
    public GameObject characterSelectScreen;

    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

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
        if (Input.GetMouseButtonDown(0))
            clicked = true;
        clicked = false;

        if(clicked)
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                Debug.Log("Hit " + result.gameObject.name);

            }
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
