using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionGroundCheck : MonoBehaviour
{

    public Obstruction parent;

    private void Start()
    {
        this.parent = this.GetComponentInParent<Obstruction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
        if(other.TryGetComponent<Ladder>(out Ladder ladder))
        {
            Debug.Log("Follow Found Ladder");
            if(parent is Follow)
            {
                Debug.Log("This Is Follow");
                Follow follow = parent as Follow;
                if (follow.isClimbing)
                {
                    follow.isClimbing = false;
                    follow.previousLadder = ladder.gameObject;
                    follow.currentLadder = null;
                }
                else 
                {
                    follow.isClimbing = true;
                    follow.accessFromTop = ladder.IsAcessFromTop(follow.gameObject);
                }
            }

        }
    }
}
