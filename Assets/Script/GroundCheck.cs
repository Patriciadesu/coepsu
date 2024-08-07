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
            if (Player.Instance.isClimbing)
            {
                Debug.Log("Exit State");
                ladder.isPlayerOnLadder = false;
                Player.Instance.ExitClimbState();
            }
            else
            {
                Player.Instance.isLadderInFront = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Ladder>(out Ladder ladder))
        {
            Player.Instance.isLadderInFront = false;
        }
    }
}
