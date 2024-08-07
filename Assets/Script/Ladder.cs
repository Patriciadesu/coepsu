using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool isPlayerOnLadder;
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
