using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject GameOverScreen;
    bool GameFinished;
    Canvas Canvas;
    private AudioSource GameOverMusic;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = false;
        GameOverMusic = GetComponent<AudioSource>();
        GameOverMusic.Stop();
    }

    // Update is called once per frame
    public void GameOver()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        GameFinished = !GameFinished;
        Canvas.enabled = GameFinished;
        Time.timeScale = (GameFinished) ? 0 : 1f;
        GameOverMusic.Play();
    }

    public void Reset()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("JohnShoots");
    }
    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
