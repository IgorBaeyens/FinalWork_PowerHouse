using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuNavigation : MonoBehaviourPunCallbacks
{
    private List<GameObject> menus = new List<GameObject>();
    private GameObject rooms, createRoom, room;
    private GameObject activeMenu;

    void Start()
    {
        foreach (Transform child in gameObject.transform)
            menus.Add(child.gameObject);

        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
            switch (menu.name)
            {
                case "---Rooms---":
                    rooms = menu;
                    break;
                case "---Create Room---":
                    createRoom = menu;
                    break;
                case "---Room---":
                    room = menu;
                    break;
            }
        }
        rooms.SetActive(true);
        activeMenu = rooms;
    }

    public void GoToMenu(string menuName)
    {
        GameObject nextMenu = gameObject.transform.Find(menuName).gameObject;
        activeMenu.SetActive(false);
        nextMenu.SetActive(true);
        activeMenu = nextMenu;
    }
}
