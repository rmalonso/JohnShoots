using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public GameObject GameOverScreen;
    bool GameFinished;
    Canvas Canvas;
    private AudioSource WinMusic;
    public AudioClip YouWinSound;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = GetComponent<Canvas>();
        Canvas.enabled = false;
        WinMusic = GetComponent<AudioSource>();
        WinMusic.Stop();
    }

    // Update is called once per frame
    public void MissionCompleted()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        GameFinished = !GameFinished;
        Canvas.enabled = GameFinished;
        Time.timeScale = (GameFinished) ? 0 : 1f;
        WinMusic.Play();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(YouWinSound);
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
