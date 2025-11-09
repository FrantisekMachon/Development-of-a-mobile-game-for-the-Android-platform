using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpactScript : MonoBehaviour
{

    AudioManager audioManager;
    [SerializeField] bool playSound = true;
    [SerializeField] bool randomSize = false;

    private void Awake()
    {
        
        if (playSound)
        {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlayUISound();
        }
        if (randomSize){
            var randomScale = Random.Range(0.5f, 1.5f);
            transform.localScale = new Vector3(randomScale,randomScale, 1);
        }

        Destroy(gameObject, 1);
    }


}
