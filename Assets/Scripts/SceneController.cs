using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private Scene currentActiveScene;
    // Start is called before the first frame update
    void Start()
    {
        currentActiveScene = SceneManager.GetActiveScene();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(currentActiveScene.buildIndex + 1);
    }

    public void RestartFromBeginning()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
