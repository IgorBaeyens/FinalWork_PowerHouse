using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//Script for the turntable-like UI in character select

public class Turntable : MonoBehaviour
{
    private Transform[] children;
    private RectTransform parent;
    private float scrollValue = 8.5f;

    void Start()
    {
        children = GetComponentsInChildren<Transform>();
        parent = GetComponent<RectTransform>();
    }

    void Update()
    {
        //reduces or increases when you use the scrollwheel
        if(Input.mouseScrollDelta.y == 1)
        {
            scrollValue -= 10;
        } else if (Input.mouseScrollDelta.y == -1)
        {
            scrollValue += 10;
        }

        //rotates object based on scroll value
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, scrollValue)), 0.1f);

        //sets children of object to 0 rotation
        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }
}
