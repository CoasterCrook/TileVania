using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endScreen;
    public static bool isPaused;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] TextMeshProUGUI endScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
        gameMusic.Play();
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        if(currentSceneIndex == 4)
        {
            TimeSpan time = TimeSpan.FromSeconds(GetComponent<GameSession>().currentTime);
            timerText.text = "Time: " + time.Minutes.ToString() + ":" + time.Seconds.ToString("00") + "." + time.Milliseconds.ToString();
            FindObjectOfType<GameSession>().StopStopWatch();
            endScreen.SetActive(true);
            endScoreText.text = "You Scored\n" + GetComponent<GameSession>().score + " / 3000 Points!";
            
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        gameMusic.Pause();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        gameMusic.Play();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        isPaused = false;
        pauseMenu.SetActive(false);
        Destroy(endScreen);
        FindObjectOfType<GameSession>().ResetGameSession();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(1);
        Destroy(gameObject);
    } 

    public void RestartGameTwo()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(1);
        ResumeGame();
        Destroy(gameObject);
    } 
}
