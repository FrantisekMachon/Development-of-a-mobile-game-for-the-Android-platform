using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingHelperManager : MonoBehaviour
{
    // Start is called before the first frame update
    float rotateSpeed = 100f;
    private GameObject player;
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();
    private bool activated = false;
    private int activeCount = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        //defaultne neaktivni
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
    }

    void Start()
    {
        //EnableSpinners();
    }

    public void EnableSpinners()
    {
        if (!activated)
            activated = true;
        //pokud jeste neni spinnujici helper aktivni tak se aktivuje
        //vzdy pri volani se aktivuji dva (po kazdem zabiti bosse)
        foreach (GameObject go in gameObjects)
        {
            if (activeCount < 2)
            {
                if (!go.activeSelf) { 
                    go.SetActive(true);
                    activeCount++;
                }
            }
                
        }
        activeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            transform.position = player.transform.position;
            transform.Rotate(new Vector3(0, 0, -1), rotateSpeed * Time.deltaTime);
        }
        
    }
}
