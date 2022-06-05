using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class FireBomb : MonoBehaviourPun
{
    private VisualEffect explosionEffect;

    void Start()
    {
        explosionEffect = GetComponentInChildren<VisualEffect>();
        Invoke("Explode", 2.5f);
    }

    private void Explode()
    {
        explosionEffect.SendEvent("Explode");
        explosionEffect.GetComponent<DestroyGameObject>().startCountdown = true;
        explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }
}
