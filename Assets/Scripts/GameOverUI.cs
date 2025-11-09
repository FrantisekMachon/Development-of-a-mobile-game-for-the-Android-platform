using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("score",0);
        int highScore = PlayerPrefs.GetInt("highScore",0);
        scoreText.text = "Game Over \n" + "Score: \n" + score.ToString("000000000");
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore",score);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
