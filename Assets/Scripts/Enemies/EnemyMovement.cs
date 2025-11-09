using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 5f;

    private bool damageDealt = false; 
    SpriteRenderer spriteRenderer;
    bool facingRight = true;

    private void Start()
    {
        
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            Movement(1,1);
    }

    private void Movement(int direction,int multiplier)
    {
        
        var pos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (pos.x < transform.position.x && facingRight)
            Flip();
        if (pos.x > transform.position.x && !facingRight)
            Flip();

        if (!(Vector2.Distance(transform.position, pos) < 0.2f))
        {
            //pohyb smerem k hraci
            var step = enemySpeed * Time.deltaTime * direction * multiplier;
            transform.position = Vector2.MoveTowards(transform.position, pos, step);
        }
    }

    private void Flip()
    {
        //otaci smerem k hraci
        var currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (!damageDealt)
            {
                collision.GetComponent<PlayerStats>().DecreasePlayerCurrentHealth();
                damageDealt = true;
                //presune enemy mimo zonu hrace
                Movement(-1, 60);
            }
        }
    }

    private void OnTriggerExit2D()
    {
        damageDealt = false;
    }

}
