using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Sprite[] menuImage;
    public Image image;
    public int menuIndex = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if(menuIndex == 2)
            {
                menuIndex = 0;
            }
            else
            {
                menuIndex += 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(menuIndex == 0)
            {
                menuIndex = 2;
            }
            else
            {
                menuIndex--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(menuIndex == 0)
            {
                GameManager.Instance.ResetGame();
            }
            else if(menuIndex == 1)
            {
                Application.Quit();
            }
        }
        image.sprite = menuImage[menuIndex];
    }

}
