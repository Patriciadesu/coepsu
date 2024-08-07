using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLevelManager : MonoBehaviour
{
    public Animator anim;
    void Update()
    {
        if (FindObjectsOfType<VanishFloor>() == null)
        {
            anim.SetTrigger("Win"); 
        }   
    }
    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
    public void DestroyAllObstruction()
    {
        Obstruction[] obstructions = FindObjectsOfType<Obstruction>();
        foreach(Obstruction obstruction in obstructions)
        {
            Destroy(obstruction.gameObject);
        }

    }
}
