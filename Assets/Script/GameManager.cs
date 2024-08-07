using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonPersistent<GameManager>
{
    private int currentLevel = 1;
    private string sceneName = "Level_1";

    public int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
            sceneName = "Level_" + value.ToString();  // Added underscore to match scene names
        }
    }

    private int lives = 4;
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            Debug.Log("Lives Remaining: " + lives.ToString());
            if (lives <= 0)  // Trigger game over when lives are 0
            {
                GameOver();
            }
        }
    }

    public int score;
    public float time;

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void ResetLevel()
    {
        Lives -= 1;
        if (lives > 0)
        {
            SceneManager.LoadScene(sceneName);
            time = 0;
        }
    }

    public void NextLevel()
    {
        CurrentLevel += 1;
        string nextSceneName = "Level_" + CurrentLevel.ToString();

        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
            time = 0;
        }
        else
        {
            Win();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    public void ResetGame()
    {
        CurrentLevel = 1;  // Start from Level 1
        Lives = 4;
        ResetLevel();
    }
}
