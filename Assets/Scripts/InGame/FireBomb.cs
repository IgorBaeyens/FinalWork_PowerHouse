using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class FireBomb : MonoBehaviourPun
{
    private VisualEffect explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        explosionEffect = GetComponentInChildren<VisualEffect>();
        Invoke("Explode", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (!other.gameObject.GetPhotonView().IsMine)
            //{
            //Explode();
            //}
        }
    }

    private void Explode()
    {
        explosionEffect.SendEvent("Explode");
        explosionEffect.GetComponent<DestroyGameObject>().startCountdown = true;
        explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
