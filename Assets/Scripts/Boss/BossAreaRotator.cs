using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;


    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }
}
