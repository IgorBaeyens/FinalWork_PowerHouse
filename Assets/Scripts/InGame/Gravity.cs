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
    private Vector3 storedPlayerForward;
    public float storedPlayerRotation;

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
            gravityVector = other.transform.Find("Gravity Vector");

            if (transform.up != gravityVector.up)
            {

                if (gameObject.CompareTag("Player"))
                {
                    playerFP = gameObject.transform.Find("First Person View");
                    storedPlayerForward = playerFP.forward;
                }
                
                Quaternion rotation = Quaternion.FromToRotation(transform.up, gravityVector.up) * transform.rotation;

                //TODO: mouse movement cannot interupt this, it causes jittering
                iTween.RotateTo(gameObject, rotation.eulerAngles, 0.5f);

                if (gameObject.CompareTag("Player"))
                {
                    Quaternion rotationFP = Quaternion.FromToRotation(playerFP.forward, storedPlayerForward) * playerFP.rotation;
                    iTween.RotateTo(playerFP.gameObject, rotation.eulerAngles, 0.5f);
                }
            }
        }
    }
}
