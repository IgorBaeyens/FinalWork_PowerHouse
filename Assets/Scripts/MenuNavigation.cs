using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuNavigation : MonoBehaviourPunCallbacks
{

    private List<GameObject> menus = new List<GameObject>();
    public GameObject activeMenu;

    void Start()
    {
        foreach (Transform child in gameObject.transform)
            menus.Add(child.gameObject);
        foreach (GameObject menu in menus)
            menu.SetActive(false);

        activeMenu.SetActive(true);
    }

    public void GoToMenu(string menuName)
    {
        GameObject nextMenu = gameObject.transform.Find(menuName).gameObject;
        activeMenu.SetActive(false);
        nextMenu.SetActive(true);
        activeMenu = nextMenu;
    }

}
