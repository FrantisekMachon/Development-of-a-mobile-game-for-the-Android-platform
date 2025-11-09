using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    float angle;
    private GameObject helper;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    public float GetAngle()
    {
        return angle;
    }

    // Update is called once per frame
    void Update()
    {
        //hledání helpera
        helper = GameObject.FindWithTag("Helper");
        if (helper != null)
        {
            //zobrazení objektu
            transform.localScale = scale;
            //výpoèet úhlu pro správnou rotaci a následné pøedání
            var tempAngle = helper.transform.position - transform.position;
            angle = Mathf.Atan2(tempAngle.y, tempAngle.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            //nejjednodušší zpùsob jak vizuálnì skrýt objekt
            transform.localScale = new Vector3(0, 0, 0);
        }
            
         
    }
}
