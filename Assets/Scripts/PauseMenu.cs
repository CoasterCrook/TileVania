using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject endScreen;
    public static bool isPaused;
    [SerializeField] AudioSource gameMusic;
    [SerializeField] TextMeshProUGUI endScoreText;
    
    void Start()
    {
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
        gameMusic.Play();
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (Input.GetKeyDown(KeyCode.Escape))
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
}
