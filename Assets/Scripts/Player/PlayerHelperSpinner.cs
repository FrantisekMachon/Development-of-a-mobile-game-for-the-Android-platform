using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelperSpinner : MonoBehaviour
{
    float rotateAroundSpeed = 5f;
    float rotateSpeed = 180f;
    List<GameObject> gameObjects = new List<GameObject>();
    private bool inProgress = false;
    [SerializeField] new ParticleSystem particleSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //particleSystem.Play();
            collision.GetComponent<EnemyStats>().DecreaseCurrentHealth();
        }
        
    }



    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -1), rotateSpeed * Time.deltaTime);
        //transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), rotateAroundSpeed * Time.deltaTime);
    }
}
