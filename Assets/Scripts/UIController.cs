using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject nextLevelButton;
    public GameObject tutorialUI;
    public Text levelClearText;

    private Scene currentActiveScene;
    private SceneController mySceneController;

    // Start is called before the first frame update
    void Start()
    {
        currentActiveScene = SceneManager.GetActiveScene();
        mySceneController = GameObject.Find("Scene Manager").GetComponent<SceneController>();
        TutorialScene();
    }

    private void TutorialScene()
    {
        if (currentActiveScene.name == "Level 1")
        {
            tutorialUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        levelClearText.text = "Pause Game";
        Time.timeScale = 0;
        nextLevelButton.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentActiveScene.name);
    }

    public void EndGame()
    {
        if(currentActiveScene.buildIndex + 1 == SceneManager.sceneCountInBuildSettings - 1)
        {
            NextLevel();
        } else
        {
            pausePanel.SetActive(true);
            nextLevelButton.SetActive(true);
            resumeButton.SetActive(false);
            levelClearText.text = "Level Clear!";
        }
    }

    public void FailedGame() // Tidak dipakai
    {
        pausePanel.SetActive(true);
        resumeButton.SetActive(false);
        levelClearText.text = "Try Again";
    }

    public void NextLevel()
    {
        mySceneController.NextScene();
    }

    public void BackToMenu()
    {
        ResumeGame();
        mySceneController.MainMenu();
    }

}