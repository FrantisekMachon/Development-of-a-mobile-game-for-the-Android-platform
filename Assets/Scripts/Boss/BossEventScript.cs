using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventScript : MonoBehaviour
{
    private bool hasStarted = false;
    private bool bossDied = false;
    private bool playedDeathEffect = false;
    private KillzoneScript killzoneScript;
    [SerializeField] ParticleSystem particleSystemExplosion;
    [SerializeField] GameObject boss;
    private AudioManager audioManager;
    private BossScript bossScript;

    private void Awake()
    {
        killzoneScript = FindObjectOfType<KillzoneScript>();
        audioManager = FindObjectOfType<AudioManager>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        bossScript = FindObjectOfType<BossScript>();
    }

    public void SetHasStarted()
    {
        hasStarted = !hasStarted;
    }

    public bool GetHasStarted()
    {
        return hasStarted;
    }

    public void SetBossDied()
    {
        bossDied = true;
    }

    private void SpawnBoss()
    {
        Instantiate(boss);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted && !bossDied)
        {
            return;
        }
        else if (hasStarted && !bossDied)
        {
            
            if (killzoneScript.GetBossEventState() == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                audioManager.PlayBossSpawnSound();
                particleSystemExplosion.Play();
                killzoneScript.ChangeBossEventState();
                Handheld.Vibrate();
                Invoke("SpawnBoss", 6f);
            }            
        }
        else if (hasStarted && bossDied)
        {
            if (!playedDeathEffect)
            {
                audioManager.PlayBossDeathSound();
                particleSystemExplosion.Play();
                Handheld.Vibrate();
                playedDeathEffect = true;
            }
            
            if (killzoneScript.GetBossEventState() == 1)
            {
                killzoneScript.ChangeBossEventState();
            }
            if (killzoneScript.GetBossEventState() == 0)
            {
                hasStarted = false;
                bossDied = false;
                playedDeathEffect = false;
            }

        }
    }
}
