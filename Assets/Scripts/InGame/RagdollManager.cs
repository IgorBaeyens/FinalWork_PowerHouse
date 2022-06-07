using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    private Collider[] ragdollColliders;
    public Animator thirdPersonAnimator;
    private GameObject playerObject;
    private Gravity playerGravity;
    private Collider playerCollider;

    void Start()
    {
        ragdollColliders = GetComponentsInChildren<Collider>();
        thirdPersonAnimator = GetComponent<Animator>();
        playerObject = transform.parent.gameObject;
        playerGravity = playerObject.GetComponent<Gravity>();
        playerCollider = playerObject.GetComponent<Collider>();

        TurnOffRagdoll();
    }

    private void Update()
    {
        foreach (Collider collider in ragdollColliders)
        {
            if (!collider.isTrigger)
            {
                collider.attachedRigidbody.AddForce(playerObject.transform.up * -15);
            }
        }
    }

    public void TurnOffRagdoll()
    {
        foreach (Collider collider in ragdollColliders)
        {
            collider.isTrigger = true;
            collider.attachedRigidbody.velocity = Vector3.zero;
        }

        playerGravity.enabled = true;
        thirdPersonAnimator.enabled = true;
        playerCollider.enabled = true;
        playerCollider.attachedRigidbody.isKinematic = false;
        playerCollider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        playerCollider.attachedRigidbody.velocity = Vector3.zero;

    }

    public void TurnOnRagdoll()
    {
        playerGravity.enabled = false;
        thirdPersonAnimator.enabled = false;
        playerCollider.attachedRigidbody.isKinematic = true;
        playerCollider.attachedRigidbody.velocity = Vector3.zero;
        playerCollider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        playerCollider.enabled = false;

        foreach (Collider collider in ragdollColliders)
        {
            collider.isTrigger = false;
            collider.attachedRigidbody.velocity = Vector3.zero;
        }

    }

}
