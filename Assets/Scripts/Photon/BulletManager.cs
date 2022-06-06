using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BulletManager : MonoBehaviour
{
    private int ownerId;
    private string ownerTeam;
    private string ownerName;
    private float damage = 150;
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
    public string setOwnerTeam(string newOwnerTeam)
    {
        return ownerTeam = newOwnerTeam;
    }
    public string setOwnerName(string newOwnerName)
    {
        return ownerName = newOwnerName;
    }

    public float getOwnerId()
    {
        return ownerId;
    }
    public string getOwnerTeam()
    {
        return ownerTeam;
    }
    public string getOwnerName()
    {
        return ownerName;
    }
    public float getDamage()
    {
        return damage;
    }
}
