using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//apply gravity to gameobject
//when gameobject enters trigger with tag GravityTrigger align to y axis of the gravity vector.

public class Gravity : MonoBehaviour
{
    private PlayerMovement movementScript;

    private new Rigidbody rigidbody;
    public bool insideGravityTrigger = false;
    private Transform gravityVector;
    private Transform playerFP;
    private Vector3 storedObjectForward;
    private Vector3 storedPlayerForward;
    public Vector3 storedPlayerRotation;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
    }

    void Update()
    {
        //rigidbody.AddRelativeForce(new Vector3(0, -15, 0));
    }

    private void FixedUpdate()
    {
        rigidbody.AddRelativeForce(new Vector3(0, -19, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GravityTrigger"))
        {
            gravityVector = other.transform.Find("Gravity Vector");

            float angle = Vector3.Angle(transform.up, gravityVector.up);
            if (angle > 5)
            {
                storedObjectForward = transform.forward;
                

                if (gameObject.CompareTag("Player"))
                {
                    playerFP = gameObject.transform.Find("First Person View");
                    storedPlayerForward = playerFP.forward;
                    storedPlayerRotation = playerFP.eulerAngles;
                }

                Quaternion rotation;
                if (angle >= 180)
                {
                    rotation = Quaternion.LookRotation(storedObjectForward, gravityVector.up);
                } else
                {
                    rotation = Quaternion.FromToRotation(transform.up, gravityVector.up) * transform.rotation;
                }

                iTween.RotateTo(gameObject, rotation.eulerAngles, 1f);

                if (gameObject.CompareTag("Player"))
                {
                    iTween.RotateTo(playerFP.gameObject, rotation.eulerAngles, 1f);
                }
            }
        }
    }
}
