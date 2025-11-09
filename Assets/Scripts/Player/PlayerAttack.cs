using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] GameObject bullet;
    [SerializeField] Animator animator;
    [SerializeField] TrailRenderer trailRenderer;
    float playerRange = 4f;
    private float timeBetweenAttacks = 0f;
    private float animationTime = 0f;
    private bool playAnimation = false;
    private GameObject[] Enemies;
    private Transform closestEnemy;
    private AudioManager audioManager;
    private Color ogColor;
    private Color newColor;

    private bool fired = false;
    


    
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        ogColor = trailRenderer.startColor;
        newColor = new Color(0.5f, 0.75f, 0.9f);
        //audioManager.PlaySpawnSound();
    }


    // Update is called once per frame
    void Update()
    {
        timeBetweenAttacks += Time.deltaTime;


        if(timeBetweenAttacks >= stats.GetPlayerAttSpeed())
        {
            fired = false;
            GetClosestEnemy();
            
            if (!fired)
                timeBetweenAttacks = 0f + stats.GetPlayerAttSpeed() * 3/4f;
            else 
                timeBetweenAttacks = 0f;
            
            
        }
        
        


    }

    private void Shoot(Transform shootAtPos)
    {
        animator.Play("Attack");
        audioManager.PlayShootSound();
        ChangeTrailColor();
        //výpoèet úhlu mezi hráèem a enemy postavou 
        //pro správnou orientaci a smìr projektilu
        var tempAngle = shootAtPos.position - transform.position;
        float angle = Mathf.Atan2(tempAngle.y, tempAngle.x) * Mathf.Rad2Deg - 90;

        //pøi upgradu se zvyšuje poèet projektilù které hlavní postava vystøelí
        //je potøeba zajistit aby vždy jeden projektil letìl na nepøítele
        //zbytek se rozmístí pod úhlem okolo centrálního projektilu
        //playerDmg uschovává poèet projektilù
        int spread = stats.GetPlayerDmg() / 2;
        for (int i = -spread; i < spread + (stats.GetPlayerDmg() % 2); i++)
        {
            Instantiate(bullet, transform.position, 
                Quaternion.AngleAxis(angle + i * 20f, Vector3.forward));
        }
        fired = true;
    }

    private void ChangeTrailColor()
    {
        trailRenderer.startColor = newColor;
        trailRenderer.endColor = newColor;
        StartCoroutine(ColorBack());

    }

    IEnumerator ColorBack()
    {
        yield return new WaitForSeconds(0.15f);
        trailRenderer.startColor = ogColor;
        trailRenderer.endColor = ogColor;
    }

    private void GetClosestEnemy() 
    {
        //seznam nepøátel
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(Enemies.Length > 0)
        {
            //uschování range hráèe pro další testování
            float closestDistance = playerRange;
            closestEnemy = null;
            foreach (GameObject enemy in Enemies)
            {
                float currDist = Vector2.Distance(transform.position, 
                    enemy.transform.position);
                //pokud se nachází enemy v range playera je jeho pozice
                //uložena jako nejbližší a dochází k dalšímu porovnání
                if (currDist < closestDistance)
                {
                    closestDistance = currDist;
                    closestEnemy = enemy.transform;
                }
            }

            //pokud byl nalezen aspoò jeden enemy v player range 
            //tak je na pozici nejbližšího enemy vystøelen projektil
            if (closestDistance < playerRange)
                Shoot(closestEnemy);
        }
    }
}
