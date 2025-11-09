using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public static bool pauseRequest = false;
    public static bool buttonPressed = false;

    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject MainMenuUI;
    [SerializeField] GameObject JoystickUI;
    [SerializeField] GameObject InfoUI;
    [SerializeField] TextMeshProUGUI cashText;
    private UIScript uiScript;
    

    public void SetIsPaused()
    {
        //podle kontextu pause/unpause hry
        pauseRequest = !pauseRequest;
        buttonPressed = true;
    }


    private void Awake()
    {
        uiScript = FindObjectOfType<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //podle kontextu pause/unpause hry
        if (buttonPressed)
        {
            if (pauseRequest)
                Pause();
            else
                Resume();
        }
        if (cashText.isActiveAndEnabled)
        {
            cashText.text = "CASH: " + uiScript.getCash().ToString("000") + "$";
        }
    }

    private void Pause()
    {
        PauseMenuUI.SetActive(true);
        MainMenuUI.SetActive(false);
        JoystickUI.SetActive(false);
        InfoUI.SetActive(false);

        Time.timeScale = 0f;
        buttonPressed = false;
    }

    private void Resume()
    {
        //Handheld.Vibrate();
        PauseMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
        JoystickUI.SetActive(true);
        InfoUI.SetActive(true);
        Time.timeScale = 1f;
        buttonPressed = false;
    }
}
