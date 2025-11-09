using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    [SerializeField] float delayTime = 5f;

    public void LoadGame()
    {
        Handheld.Vibrate();
        SceneManager.LoadScene("MainGame");
    }

    public void LoadMainMenu()
    {
        Handheld.Vibrate();
        SceneManager.LoadScene("MainMenu");
                  
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting");
    }

    public void LoadGameOver()
    {
        StartCoroutine(Wait("GameOverMenu", delayTime));
    }

    IEnumerator Wait(string scene,float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
