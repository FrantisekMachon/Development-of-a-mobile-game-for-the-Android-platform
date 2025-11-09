using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class KillzoneScript : MonoBehaviour
{

    private float changeAmount = 0.5f;
    private bool shrinking = true;
    private int bossEventState = 0;


    void Update()
    {
        //pokud je boss event tak se zmensi na pozadovanou velikost
        //po jeho konci zase na puvodni velikost
        //case 0 - neni boss event, case 1 je boss event, case 2 boss event konci a zmena na puvodni state (case0)
        switch(bossEventState)
        {
            case 0:
                if (transform.localScale.x > 20 && shrinking)
                {
                    transform.localScale -= new Vector3(changeAmount * Time.deltaTime, changeAmount * Time.deltaTime, 0);
                }
                if (transform.localScale.x <= 20 || !shrinking)
                {
                    shrinking = false;

                    transform.localScale += new Vector3(changeAmount * Time.deltaTime, changeAmount * Time.deltaTime, 0);
                }
                if (transform.localScale.x > 25)
                {
                    shrinking = true;
                }
                break;
            case 1:
                if (transform.localScale.x > 10)
                {
                    transform.localScale -= new Vector3(changeAmount * Time.deltaTime *3f, changeAmount * Time.deltaTime * 3f, 0);
                }
                break;
            case 2:
                if (transform.localScale.x <= 25)
                {
                    transform.localScale += new Vector3(changeAmount * Time.deltaTime * 3f, changeAmount * Time.deltaTime * 3f, 0);
                }
                else
                {
                    bossEventState = 0;
                }
                break;

        }
        transform.Rotate(new Vector3(0, 0, 1), changeAmount * Time.deltaTime * -50f);

    }

    public void ChangeBossEventState()
    {
        bossEventState++;
    }

    public int GetBossEventState()
    {
        return bossEventState;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().InstantDeath();
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

    }
}
