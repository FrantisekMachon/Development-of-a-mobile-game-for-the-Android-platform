using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingScript : MonoBehaviour
{

    private PlayerStats playerStats;
    private AudioManager audioManager;
    private UIScript uiScript;
    [SerializeField] new ParticleSystem particleSystem;
    private bool actionFinished = false;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        audioManager = FindObjectOfType<AudioManager>();
        uiScript = FindObjectOfType<UIScript>();    
    }

   //bud prida hraci hp nebo penize za sebrani
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !actionFinished)
        {
            if (playerStats.GetPlayerCurrentHealth() < playerStats.GetPlayerBaseHealth())
            {
                playerStats.HealPlayer();
                audioManager.PlayHealSound();
            }
            else
            {
                audioManager.PlayAttachSound();
                uiScript.addCash(5);
            }
            actionFinished = true;
            particleSystem.Play();
            transform.localScale = new Vector3(0, 0, 0);
            Destroy(gameObject, 1f);
        }
        
    }
}
