using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void TryAgain()
    {
        GameManager.Instance.ResetGame();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
