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
    private ScoreManager scoreManager;
    private SpawnPlayers spawnPlayers;
    private RagdollManager ragdollManager;
    private PlayerControls playerControls;
    private CharacterScript characterScript;
    private Character character;
    private SkinnedMeshRenderer[] firstPersonMesh;
    private SkinnedMeshRenderer[] thirdPersonMesh;

    private float maxHealth;
    public float currentHealth = 1f;
    private float respawnTime = 8f;
    private bool isDead = false;
    private bool respawned = false;

    private TMP_Text healthBarText;
    private Image healthBarLight;
    private Image healthBarDark;

    void Start()
    {
        if (photonView.IsMine)
        {
            scoreManager = FindObjectOfType<ScoreManager>();
            spawnPlayers = GameObject.Find("SPAWN_PLAYERS").GetComponent<SpawnPlayers>();
            characterScript = GetComponent<CharacterScript>();
            playerControls = GetComponent<PlayerControls>();
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
            ragdollManager = GetComponentInChildren<RagdollManager>();
            ragdollManager.TurnOnRagdoll();
            if (photonView.IsMine)
            {
                Die();
            }
            Invoke("Respawn", respawnTime);
        }

        if (isDead && respawned)
        {
            isDead = false;
            respawned = false;
            ragdollManager.TurnOffRagdoll();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = GetComponent<PlayerManager>();
        string victimName = playerManager.getPlayerName();
        string victimTeam = playerManager.getPlayerTeam();
        string victimColor = playerManager.getPlayerTeamColorHEX();

        if (other.CompareTag("Bullet"))
        {
            GameObject bullet = other.transform.parent.gameObject;
            BulletManager bulletManager = bullet.GetComponent<BulletManager>();
            PlayerManager bulletOwner = PhotonView.Find(bulletManager.getOwnerId()).GetComponent<PlayerManager>();
            if (playerManager.getPlayerTeam() != bulletOwner.getPlayerTeam())
            {
                bulletManager.Explode();
                if(photonView.IsMine)
                {
                    TakeDamage(bulletManager.getDamage());
                    if (currentHealth <= 0)
                    {
                        string killerName = bulletOwner.getPlayerName();
                        string killerTeam = bulletOwner.getPlayerTeam();
                        string killerColor = bulletOwner.getPlayerTeamColorHEX();
                        string chatMessage = $"<color=#{killerColor}>{killerName}</color> has killed <color=#{victimColor}>{victimName}</color>";
                        photonView.RPC("LogDeath", RpcTarget.All, chatMessage);
                        scoreManager.IncreaseScore(killerTeam);
                    }
                }
            }
        }
        if (other.CompareTag("Kill Barrier"))
        {
            if (photonView.IsMine)
            {
                TakeDamage(currentHealth);
                string chatMessage = $"<color=#{victimColor}>{victimName}</color> fell through the map";
                photonView.RPC("LogDeath", RpcTarget.All, chatMessage);
            }
            
        }
    }



    void Die()
    {

        photonView.RPC("UpdateHealthRPC", RpcTarget.Others, photonView.ViewID, currentHealth);

        firstPersonMesh = characterScript.getFirstPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
        thirdPersonMesh = characterScript.getThirdPerson().GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (SkinnedMeshRenderer renderer in firstPersonMesh)
            renderer.enabled = false;
        foreach (SkinnedMeshRenderer renderer in thirdPersonMesh)
            renderer.renderingLayerMask = 1;

        //stop rotation after death
        photonView.RPC("RotationSync", RpcTarget.Others, photonView.ViewID);
        characterScript.getThirdPerson().transform.parent = null;

        playerControls.SetPlayerCanMove(false);
        playerControls.SetPlayerCanShoot(false);

    }

    void Respawn()
    {
        if(photonView.IsMine)
        {
            firstPersonMesh = characterScript.getFirstPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
            thirdPersonMesh = characterScript.getThirdPerson().GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer renderer in firstPersonMesh)
                renderer.enabled = true;
            foreach (SkinnedMeshRenderer renderer in thirdPersonMesh)
                renderer.renderingLayerMask -= 1;

            photonView.RPC("RotationSync", RpcTarget.Others, photonView.ViewID);
            characterScript.getThirdPerson().transform.SetParent(gameObject.transform);

            playerControls.SetPlayerCanMove(true);
            playerControls.SetPlayerCanShoot(true);

            ResetHealth();
            spawnPlayers.Respawn(gameObject);
            photonView.RPC("UpdateHealthRPC", RpcTarget.Others, photonView.ViewID, currentHealth);
            UpdateHealthGraphic(currentHealth, currentHealth);
            photonView.RPC("UpdatePlayerState", RpcTarget.All, photonView.ViewID, true);
        }
       



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

    [PunRPC]
    void UpdatePlayerState(int playerId, bool playerState)
    {
        GameObject player = PhotonView.Find(playerId).gameObject;
        HealthManager healthManager = player.GetComponent<HealthManager>();
        healthManager.respawned = playerState;
    }
    [PunRPC]
    void LogDeath(string chatMessage)
    {
        GameObject log = GameObject.Find("Log");
        GameObject messageInstance = PhotonNetwork.Instantiate("Message", log.transform.position, Quaternion.identity);
        messageInstance.transform.SetParent(log.transform);
        messageInstance.GetComponent<TMP_Text>().text = chatMessage;
    }
    [PunRPC]
    void UpdateHealthRPC(int playerId, float currentHealth)
    {
        HealthManager healthManager = PhotonView.Find(playerId).GetComponent<HealthManager>();
        healthManager.currentHealth = currentHealth;
    }
    [PunRPC]
    void RotationSync(int playerId)
    {
        GameObject player = PhotonView.Find(playerId).gameObject;
        PhotonTransformView photonTransform = player.GetComponent<PhotonTransformView>();
        Debug.Log(photonTransform.m_SynchronizeRotation);

        if (photonTransform.m_SynchronizeRotation)
            photonTransform.m_SynchronizeRotation = false;
        else
            photonTransform.m_SynchronizeRotation = true;
    }

    float getHealthFromOneToZero(float healthOrDamage)
    {
        return healthOrDamage / maxHealth;
    }
}
