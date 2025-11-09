using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperAttach : MonoBehaviour
{
    bool attached = false;
    float shootAngle;
    private GameObject player;
    private Vector3 distance;
    [SerializeField] GameObject bullet; //pak presunout jinam
    private float shootTimer = 1f;
    private float elapsedTime = 0f;
    private int upgradeLevel = 0;
    private float randomSpread;
    [SerializeField] new ParticleSystem particleSystem;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool Upgradable()
    {
        return (upgradeLevel < 2);
    }


    public void UpgradeHelper()
    {
        upgradeLevel++;
        if (shootTimer >= 0.6f)
            shootTimer -= 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attached)
        {
            elapsedTime+= Time.deltaTime;
            transform.position = new Vector3(player.transform.position.x + distance.x, player.transform.position.y + distance.y, player.transform.position.z);

            if (elapsedTime > shootTimer)
            {
                //po upgradu strili random vice do stran aby hitnul vice enemy
                randomSpread = Random.Range(-upgradeLevel*10f, upgradeLevel*10f);
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(shootAngle + randomSpread, Vector3.forward));
                elapsedTime = 0;
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //rešení kolize 
        if((collision.CompareTag("Player") 
            || collision.CompareTag("AttachedHelper")) 
            && !attached)
        {
            //pøedání angle
            shootAngle = FindObjectOfType<RotationTracker>().GetAngle();
            //uložení vzdálenosti od hráèe, je dále používáno pro
            //výpoèet správné pozice v metodì update
            player = GameObject.Find("Player");
            distance = transform.position - player.transform.position;
            attached = true;
            //zmìnìný tag je použit pøi upgradu turret
            //tak aby nebyly upgradovany turrety ještì nesebrané
            gameObject.tag = "AttachedHelper";
            audioManager.PlayAttachSound();
            particleSystem.Play();
        }
    }
}
