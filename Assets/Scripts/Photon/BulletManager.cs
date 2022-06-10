using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletManager : MonoBehaviour
{
    private int ownerId;
    private float damage;
    public bool activateExplosion = false;

    private VisualEffect explosionEffect;

    void Start()
    {
        explosionEffect = GetComponentInChildren<VisualEffect>();
        Invoke("Explode", 2.5f);
    }

    private void Update()
    {
        if (activateExplosion)
            Explode();
    }

    public void Explode()
    {
        explosionEffect.SendEvent("Explode");
        explosionEffect.GetComponent<DestroyGameObject>().startCountdown = true;
        explosionEffect.transform.parent = null;
        Destroy(gameObject);
    }

    public int setOwnerId(int newOwnerId)
    {
        return ownerId = newOwnerId;
    }
    public int getOwnerId()
    {
        return ownerId;
    }

    public float setDamage(float newDamage)
    {
        return damage = newDamage;
    }
    public float getDamage()
    {
        return damage;
    }
}
