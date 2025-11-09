using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFaceRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    private GameObject player;

    private Quaternion lookRotation;
    private Vector2 direction;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //otaci bossovo oblicej za hracem
        float angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}
