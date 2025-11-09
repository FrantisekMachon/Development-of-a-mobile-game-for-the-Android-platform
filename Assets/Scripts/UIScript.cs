using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{

    [SerializeField] Slider hpBarSlider;
    private int playerBaseHP = 0;
    private int playerCurrentHP = 0;
    private PlayerStats playerStats;
    private EnemySpawner enemySpawner;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI cashText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI fpsText;

    private int score = 0;
    private int cash = 0;

    public float fps = 1;
    



    public void addCash(int newCash)
    {
        cash += newCash;
    }

    public void addScore(int newScore)
    {
        score += newScore;
    }

    public int getScore()
    {
        return score;
    }
    public int getCash()
    {
        return cash;
    }

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBaseHP = playerStats.GetPlayerBaseHealth();
        InvokeRepeating("GetFPS", 0.1f, 0.1f);
        
    }

    public void GetFPS()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = fps + " FPS";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("000000000");
        cashText.text = "SHOP: " + cash.ToString("000") + "$";
        timerText.text = ConvertTime(enemySpawner.GetTimer());
        hpText.text = "HP: " + playerStats.GetPlayerCurrentHealth().ToString("00");

    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
        Debug.Log(score);
    }

    public string ConvertTime(int time)
    {
        float minutes = Mathf.FloorToInt((float)time / 60);
        float seconds = Mathf.FloorToInt((float)time % 60);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
