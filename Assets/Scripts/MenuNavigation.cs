using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject mainScreen;
    public GameObject characterSelectScreen;

    private bool clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
        mainScreen.SetActive(false);
        characterSelectScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            clicked = true;
        clicked = false;

        if(startScreen.activeSelf)
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
