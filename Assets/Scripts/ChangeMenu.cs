using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    public void m_DisableMenu(string menuName)
    {
        GameObject nextMenu = gameObject.transform.Find(menuName).gameObject;
        nextMenu.SetActive(false);
    }

    public void m_ActivateMenu(string menuName)
    {
        GameObject nextMenu = gameObject.transform.Find(menuName).gameObject;
        nextMenu.SetActive(true);
    }
}
