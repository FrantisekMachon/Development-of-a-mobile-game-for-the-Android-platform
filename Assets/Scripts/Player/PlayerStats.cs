using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int baseHealth = 10;
    private int playerCurrentHealth;
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] GameObject deathEffectObject;

    private List<GameObject> inTriggerZone;
    private AudioManager audioManager;

    
    float playerBaseAttSpeed = 2f;
    int playerBaseDmg = 1;

    private int playerDmg;
    private float playerAttSpeed;

    LevelManager levelManager;


    public void HealPlayer()
    {
        if(playerCurrentHealth <= baseHealth - 3)
        {
            playerCurrentHealth += 3;
        }
        else
        {
            playerCurrentHealth = baseHealth;
        }
    }

    public int GetPlayerBaseHealth()
    {
        return baseHealth;
    }

    public int GetPlayerCurrentHealth()
    {
        return playerCurrentHealth;
    }


    public void DecreasePlayerCurrentHealth()
    {
        playerCurrentHealth--;
        particleSystem.Play();
        Handheld.Vibrate();
        //Debug.Log(playerCurrentHealth);
    }

    public void InstantDeath()
    {
        Handheld.Vibrate();
        playerCurrentHealth = 0;
    }

    public int GetPlayerDmg()
    {
        return playerDmg;
    }

    public float GetPlayerAttSpeed()
    {
        return playerAttSpeed;
    }
    public void UpgradePlayerAttSpeed()
    {
        if (playerAttSpeed > 0.5f)
            playerAttSpeed -= 0.5f;
    }

    public void UpgradePlayerDmg()
    {
        playerDmg++;
    }


    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Handheld.Vibrate();
        playerDmg = playerBaseDmg;
        playerAttSpeed = playerBaseAttSpeed;
        playerCurrentHealth = baseHealth;
        audioManager.PlaySpawnSound();


    }

    void Update()
    {
        if(playerCurrentHealth <= 0)
        {
            //smrt
            gameObject.SetActive(false);
            audioManager.PlayDeathSound();
            Instantiate(deathEffectObject, transform.position,transform.rotation);
            levelManager.LoadGameOver();
        }
        
    }

   
}
