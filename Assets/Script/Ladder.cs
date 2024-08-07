using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool isPlayerOnLadder;

    private void Update()
    {
        if (isPlayerOnLadder)
        {
            Player.Instance.Climb(this.gameObject);
        }
    }
    public bool IsAcessFromTop(GameObject target)
    {
        if(target.transform.position.y < this.transform.position.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
