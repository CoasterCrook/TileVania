using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    [SerializeField] public int score = 0;
    
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI deathScoreText;
    [SerializeField] float deathDelay = 1f;

    public GameObject deathScreen;
    public bool startingStopWatch = false;
    public float currentTime;
    
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
        deathScreen.SetActive(false);
        currentTime = 0;
        startingStopWatch = true;
    }

    void Update() 
    {
        if (startingStopWatch == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString("00") + "." + time.Milliseconds.ToString();
    }
    

    public IEnumerator ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            yield return new WaitForSecondsRealtime(deathDelay);
            TakeLife();
        }
        else
        {
            TakeLastLife();
            yield return new WaitForSecondsRealtime(deathDelay);
            StopStopWatch();
            deathScreen.SetActive(true);
            deathScoreText.text = "You Got " + GetComponent<GameSession>().score + " Points";
        }
    }

    void TakeLastLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void StartStopWatch()
    {
        startingStopWatch = true;
    }

    public void StopStopWatch ()
    {
        startingStopWatch = false;
    }
}
