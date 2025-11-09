using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Joystick joystick;

    [SerializeField] private float playerSpeed = 10f;
    [SerializeField] Animator animator;

    private float moveAmountX;
    private float moveAmountY;
    private bool facingRight = false;

    
    void Start()
    {
        /*
        var vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.LookAt = gameObject.transform;
        vcam.Follow = gameObject.transform;*/
    }

    private void Awake()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    void Update()
    {
        if (joystick.Horizontal < 0 && facingRight)
            Flip();
        if (joystick.Horizontal > 0 && !facingRight)
            Flip();


        moveAmountX = joystick.Horizontal * playerSpeed * Time.deltaTime;
        moveAmountY = joystick.Vertical * playerSpeed * Time.deltaTime;
        
        transform.Translate(moveAmountX, moveAmountY, 0);
    }

    private void Flip()
    {
        //otoci sprite hrace smerem k pohybu
        var currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

    }

}
