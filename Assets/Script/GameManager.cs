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
            sceneName = "Level" + value.ToString();
        }
    }

    private int live = 4;
    public int Live
    {
        get
        {
            return live;
        }
        set
        {
            live = value;
            if (live < 0)
            {
                GameOver();
            }
        }
    }

    public int score;
    public float time;

    public void Update()
    {
        time += Time.deltaTime;
    }

    public void ResetLevel()
    {
        Live -= 1;
        SceneManager.LoadScene(sceneName);
        time = 0;
    }
    public void NextLevel()
    {
        try
        {
            CurrentLevel += 1;
            SceneManager.LoadScene(sceneName);
            time = 0;
        }
        catch (Exception e)
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

}
