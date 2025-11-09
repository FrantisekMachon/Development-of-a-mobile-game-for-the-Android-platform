using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 1f;
    [SerializeField] string whoToHit;
    [SerializeField] float range = 5f;
    [SerializeField] GameObject hitEffect;
    Vector3 startPos;

    private bool damageDealt = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        
        transform.position += transform.up * Time.deltaTime * bulletSpeed;
        if (Vector3.Distance(startPos,transform.position) > range)
        {
            Destroy(gameObject);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(whoToHit)) //dava dmg podle toho s kym ma kolidovat
        {
            if (!damageDealt)
            {
                damageDealt = true;
                if(whoToHit.Equals("Enemy"))
                    collision.GetComponent<EnemyStats>().DecreaseCurrentHealth();
                else
                    collision.GetComponent<PlayerStats>().DecreasePlayerCurrentHealth();
                Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                
            }
            
        }
    }

}
