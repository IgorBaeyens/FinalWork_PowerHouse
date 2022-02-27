using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        if(Input.mouseScrollDelta.y == 1)
        {
            scrollValue -= 10;
        } else if (Input.mouseScrollDelta.y == -1)
        {
            scrollValue += 10;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, scrollValue)), 0.1f);

        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                child.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }
}
