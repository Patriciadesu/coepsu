using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingFloor : MonoBehaviour
{
    public int direction;
    public int force;
    public int externalForce;

    private void Start()
    {
        externalForce = force * direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name=="groundCheck")
        {
            Debug.Log("Playerrr");
            Player.Instance.externalForce += externalForce;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "groundCheck")
        {
            Player.Instance.externalForce -= externalForce;
        }
    }

}
