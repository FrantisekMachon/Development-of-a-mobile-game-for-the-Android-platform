using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossScript : MonoBehaviour
{
    [SerializeField] EnemyStats enemyStats;
    [SerializeField] GameObject hider;
    SpriteRenderer spriteRenderer;
    BossEventScript bossEventScript;
    [SerializeField] GameObject bullet;
    AudioManager audioManager;
    float fireDelay = 2.5f;
    RotatingHelperManager rotatingHelperManager;

    private void Awake()
    {
        bossEventScript = FindObjectOfType<BossEventScript>();
        audioManager = FindObjectOfType<AudioManager>();
        rotatingHelperManager = FindObjectOfType<RotatingHelperManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = hider.GetComponent<SpriteRenderer>();
        InvokeRepeating("Attack", 1f, fireDelay);

    }

    private void Attack()
    {
        //rozhodnout ktery attack provest
        int decision = Random.Range(1, 4);
        audioManager.PlayBossShootSound();
        for (int i = 0; i < 10; i++)
         {
             for (int j = 0; j < 4; j++)
             {
                //strili bullety okolo podle ruznych patternu
                if (decision == 1)
                    StartCoroutine(FireBullet(bullet, i * 10f + j * 90, 0.2f * i));
                else if (decision == 2)
                    StartCoroutine(FireBullet(bullet, i * -10f + j * 90, 0.2f * i));
                else
                {
                    StartCoroutine(FireBullet(bullet, i * 36f + j*10f  , 0.4f * j));
                }
                    
            }  
         }
    }

    // Update is called once per frame
    void Update()
    {
        var hpPercentage = 1f - (enemyStats.GetCurrentHealth() / ((float)enemyStats.GetBaseHealth()*(float)enemyStats.GetHealthMultiplier()));
        //pres bosse je polozen sprite kteremu se s bossovo ubyvajicim hp zmensuje transparentnost a boss tak zanika do temnoty
        spriteRenderer.color = new Color(0, 0, 0, hpPercentage);
        
    }

    IEnumerator FireBullet(GameObject projectile, float angle, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
    }

    private void OnDisable()
    {
        bossEventScript.SetBossDied();
        rotatingHelperManager.EnableSpinners();
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().InstantDeath();
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
