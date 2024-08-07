using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text lifeText;
    public TMP_Text timeText;

    private void Update()
    {
        lifeText.text = "Life Remain : " + GameManager.Instance.Lives.ToString();
        timeText.text = GameManager.Instance.time.ToString();
    }

}
