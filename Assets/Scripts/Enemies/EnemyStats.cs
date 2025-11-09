using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] int baseHealth;
    int healthUpgradeMultiplier = 1;
    int currentHealth;
    [SerializeField] int scoreMultiplier = 1;
    int scoreUpgradeMultiplier = 1;
    private UIScript uiScript;

    //momentalne rozlisuje bosse od normalnich enemy, boss totiz nema umrit instantne
    [SerializeField] bool dieInstantly = true;

    private EnemySpawner enemySpawner;
    [SerializeField] GameObject deathExplosionEffect;
    float sizeMultiplier = 1;


    public void DecreaseCurrentHealth()
    {
        currentHealth--;
    }

    private void Awake()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        DoubleHPScore();
        if (dieInstantly) { 
            var randomSize = Random.Range(0.8f, 1.2f) * sizeMultiplier;
            transform.localScale = new Vector2(transform.localScale.x * randomSize, transform.localScale.y * randomSize);
        }
        currentHealth = baseHealth * healthUpgradeMultiplier;
        scoreMultiplier = scoreMultiplier * scoreUpgradeMultiplier;

        //Debug.Log(currentHealth);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetHealthMultiplier()
    {
        return healthUpgradeMultiplier;
    }

    public int GetBaseHealth()
    {
        return baseHealth;
    }

    private void DoubleHPScore()
    {
        if (enemySpawner.GetTimer() > 300)
        {
            healthUpgradeMultiplier++;
            scoreUpgradeMultiplier++;
            sizeMultiplier = 1.5f;
        }

    }


    void Update()
    {
        if(currentHealth <= 0)
        {
            
            uiScript = FindObjectOfType<UIScript>();
            uiScript.addScore(scoreMultiplier * 50);
            uiScript.addCash(scoreMultiplier);
            if (dieInstantly)
            {
                Destroy(gameObject);
                Instantiate(deathExplosionEffect, transform.position, transform.rotation);

            }
                
            else
            {
               gameObject.SetActive(false);
               Debug.Log("notDeadYet");
            }
        }
            
    }
}
