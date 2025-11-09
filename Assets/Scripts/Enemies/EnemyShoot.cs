using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private float timeBetweenAttacks = 0f;
    private float attackSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenAttacks += Time.deltaTime;
        if (timeBetweenAttacks >= attackSpeed)
        {
            Shoot(GameObject.FindGameObjectWithTag("Player").transform);
            timeBetweenAttacks = 0;
        }
    }
    private void Shoot(Transform shootAtPos)
    {
        var tempAngle = shootAtPos.position - transform.position;
        float angle = Mathf.Atan2(tempAngle.y, tempAngle.x) * Mathf.Rad2Deg - 90;
        Instantiate(bullet, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
    }
}
