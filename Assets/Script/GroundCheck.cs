using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player.Instance.isGround = true;
        Player.Instance.currentItem.Interact(collision.gameObject);
    }
    private void OnCollisionStay(Collision collision)
    {
        Player.Instance.isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        Player.Instance.isGround = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter collider");
        if (other.gameObject.TryGetComponent<Ladder>(out Ladder ladder))
        {
            Debug.Log("found ladder");
            if (ladder.isPlayerOnLadder)
            {
                Debug.Log("Exit State");
                ladder.isPlayerOnLadder = false;
                Player.Instance.ExitClimbState();
            }
            else if(Input.GetKeyDown(KeyCode.F))
            {
                Player.Instance.EnterClimbState();
                Player.Instance.Climb(ladder.gameObject);
                ladder.isPlayerOnLadder = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ladder>(out Ladder ladder))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Player.Instance.EnterClimbState();
                Player.Instance.Climb(ladder.gameObject);
                ladder.isPlayerOnLadder = true;
            }
        }
    }
}
