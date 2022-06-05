using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

// tween single value
//https://answers.unity.com/questions/336855/how-to-tween-a-variable-using-itween.html

public class HealthManager : MonoBehaviourPun
{
    private SpawnPlayers spawnPlayers;
    private RagdollManager ragdollManager;
    private PlayerMovement playerMovement;
    private AbilityHolder playerAbilities;
    private CharacterScript characterScript;
    private Character character;
    private SkinnedMeshRenderer[] firstPersonMesh;
    private SkinnedMeshRenderer[] thirdPersonMesh;

    private float maxHealth;
    private float currentHealth = 1f;
    private float respawnTime = 8f;
    private bool isDead = false;

    private TMP_Text healthBarText;
    private Image healthBarLight;
    private Image healthBarDark;

    void Start()
    {
        if (photonView.IsMine)
        {
            spawnPlayers = GameObject.Find("SPAWN_PLAYERS").GetComponent<SpawnPlayers>();
            characterScript = GetComponent<CharacterScript>();
            playerMovement = GetComponent<PlayerMovement>();
            playerAbilities = GetComponent<AbilityHolder>();
            character = characterScript.getCharacter();

            maxHealth = character.maxHealth;
            currentHealth = maxHealth;

            Transform healthHUD = GameObject.Find("Health HUD").transform;
            healthBarText = healthHUD.Find("Health Bar Text").GetComponent<TMP_Text>();
            healthBarLight = healthHUD.Find("Health Bar Light").GetComponent<Image>();
            healthBarDark = healthHUD.Find("Health Bar Dark").GetComponent<Image>();

            healthBarText.text = currentHealth.ToString();
            healthBarLight.fillAmount = getHealthFromOneToZero(currentHealth);
            healthBarDark.fillAmount = getHealthFromOneToZero(currentHealth);
        }

    }

    private void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
            Invoke("Respawn", respawnTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject bullet = other.transform.parent.gameObject;
            BulletManager bulletManager = bullet.GetComponent<BulletManager>();
            if (GetComponent<PlayerManager>().getPlayerTeam() != bulletManager.getOwnerTeam())
            {
                bulletManager.Explode();
                if(photonView.IsMine)
                {
                    TakeDamage(bulletManager.getDamage());
                }
            }
        }
    }

    [PunRPC]
    void UpdateHealthRPC(int playerViewId, float currentHealth)
    {
        HealthManager healthManager = PhotonView.Find(playerViewId).GetComponent<HealthManager>();
        healthManager.currentHealth = currentHealth;
    }

    void Die()
    {
        ragdollManager = GetComponentInChildren<RagdollManager>();
        if (photonView.IsMine)
        {
            firstPersonMesh = characterScript.getFirstPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
            thirdPersonMesh = characterScript.getThirdPerson().GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (SkinnedMeshRenderer renderer in firstPersonMesh)
                renderer.enabled = false;
            foreach (SkinnedMeshRenderer renderer in thirdPersonMesh)
            {
                Debug.Log(renderer);
                renderer.renderingLayerMask = 1;
            }

            characterScript.getThirdPerson().transform.parent = null;
            playerMovement.SetPlayerCanMove(false);
            playerAbilities.SetPlayerCanShoot(false);

            photonView.RPC("UpdateHealthRPC", RpcTarget.Others, photonView.ViewID, currentHealth);
        }
        ragdollManager.TurnOnRagdoll();
    }

    void Respawn()
    {
        ragdollManager.TurnOffRagdoll();
        if (photonView.IsMine)
        {
            firstPersonMesh = characterScript.getFirstPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
            thirdPersonMesh = characterScript.getThirdPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in firstPersonMesh)
                renderer.enabled = true;
            foreach (SkinnedMeshRenderer renderer in thirdPersonMesh)
                renderer.renderingLayerMask -= 1;

            characterScript.getThirdPerson().transform.SetParent(gameObject.transform);
            
            playerMovement.SetPlayerCanMove(true);
            playerAbilities.SetPlayerCanShoot(true);
            spawnPlayers.Respawn(gameObject);
            ResetHealth();
            photonView.RPC("UpdateHealthRPC", RpcTarget.Others, photonView.ViewID, currentHealth);
            UpdateHealthGraphic(currentHealth, currentHealth);
        }
        
        isDead = false;
    }

    void TakeDamage(float damage)
    {
        float fromHealth = currentHealth;
        currentHealth -= damage;
        float toHealth = currentHealth;
        UpdateHealthGraphic(fromHealth, toHealth);
    }

    void UpdateHealthGraphic(float fromHealth, float toHealth)
    {
        float healthBarFromHealth = getHealthFromOneToZero(fromHealth);
        float healthBarToHealth = getHealthFromOneToZero(toHealth);
        addEasing(healthBarText.gameObject, fromHealth, toHealth, 0f, "EaseHealthBarText", iTween.EaseType.easeOutQuart);
        addEasing(healthBarLight.gameObject, healthBarFromHealth, healthBarToHealth, 0f, "EaseHealthBarLight", iTween.EaseType.easeOutQuart);
        addEasing(healthBarDark.gameObject, healthBarFromHealth, healthBarToHealth, 1f, "EaseHealthBarDark", iTween.EaseType.linear);
    }

    void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBarText.text = currentHealth.ToString();
        healthBarLight.fillAmount = 1;
        healthBarDark.fillAmount = 1;
    }

    void addEasing(GameObject toAnimate, float from, float to, float delay, string callBackFunction, iTween.EaseType easing)
    {
        iTween.ValueTo(toAnimate, iTween.Hash(
            "from", from,
            "to", to,
            "delay", delay,
            "onupdatetarget", gameObject,
            "onupdate", callBackFunction,
            //"oncomplete", "",
            "easetype", easing
            )
        );
    }

    void EaseHealthBarLight(float newValue)
    {
        healthBarLight.fillAmount = newValue;
    }
    void EaseHealthBarDark(float newValue)
    {
        healthBarDark.fillAmount = newValue;
    }
    void EaseHealthBarText(float newValue)
    {
        healthBarText.text = Mathf.Round(newValue).ToString();
    }

    float getHealthFromOneToZero(float healthOrDamage)
    {
        return healthOrDamage / maxHealth;
    }
}
