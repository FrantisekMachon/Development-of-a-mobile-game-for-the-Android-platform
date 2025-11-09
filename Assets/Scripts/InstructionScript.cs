using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class InstructionScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI uiText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("tutorialWitnessed",0) == 0)
        {
            Invoke("ShowTextMove", 1f);
            Invoke("ShowTextAttack", 6f);
            Invoke("ShowTextCash", 10f);
            Invoke("ShowTextShop", 15f);
            Invoke("ShowTextHelpers", 20f);
            Invoke("ShowTextDirection", 25f);
            Invoke("ShowTextEmpty", 35f);
            
        }
        else
        {
            Invoke("ShowTextHighscore", 1f);
            Invoke("ShowTextEmpty", 10f);
        }

        
    }


    private void ShowTextHighscore()
    {
        uiText.text = "Current Highscore: " + PlayerPrefs.GetInt("highScore", 0).ToString("000000000");
    }

    private void ShowTextMove()
    {
        uiText.text = "Drag finger to move around";
    }

    private void ShowTextAttack()
    {
        uiText.text = "Fire automatically at nearby enemies";
    }

    private void ShowTextCash()
    {
        uiText.text = "Killing earns score and cash";
    }

    private void ShowTextShop()
    {
        uiText.text = "Spend cash at the shop on upgrades";
    }

    private void ShowTextHelpers()
    {
        uiText.text = "Dont forget to pickup bought turrets and spawned medkits";
    }

    private void ShowTextDirection()
    {
        uiText.text = "Turrets fire automatically in direction of indicator when picked up";
        PlayerPrefs.SetInt("tutorialWitnessed", 1);
    }

    private void ShowTextEmpty()
    {
        uiText.text = "";
        gameObject.SetActive(false);
    }

}
