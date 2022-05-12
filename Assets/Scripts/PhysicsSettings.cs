using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSettings : MonoBehaviour
{
    void Update()
    {
        Physics.Simulate(Time.deltaTime);
    }
}
