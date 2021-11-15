using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Canvas OptionsScreen;

    private void Start()
    {
        OptionsScreen = GameObject.Find("OptionsCanvas").GetComponent<Canvas>();
        OptionsScreen.enabled = false;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("JohnShoots");
    }

    public void Options()
    {
        OptionsScreen.enabled = true;
    }

    public void BackMainMenu()
    {
        OptionsScreen.enabled = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

