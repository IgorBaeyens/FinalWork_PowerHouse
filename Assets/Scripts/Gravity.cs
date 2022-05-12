using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//apply gravity to gameobject
//when gameobject enters trigger with tag GravityTrigger align to y axis of the gravity vector.

public class Gravity : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private bool insideGravityTrigger = false;
    private Transform gravityVector;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    void Update()
    {
        rigidbody.AddRelativeForce(new Vector3(0, -15, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityTrigger"))
        {
            gravityVector = other.transform.Find("Gravity Vector").transform;

            if (transform.up != gravityVector.up)
            {
                Quaternion rotation = Quaternion.FromToRotation(transform.up, gravityVector.up) * transform.rotation;
                
                //TODO: mouse movement cannot interupt this, it causes jittering
                iTween.RotateTo(gameObject, rotation.eulerAngles, 1f);
            }
        }
    }

}
