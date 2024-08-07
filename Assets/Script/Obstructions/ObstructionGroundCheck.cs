using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionGroundCheck : MonoBehaviour
{

    Obstruction parent;

    private void Start()
    {
        this.parent = this.GetComponentInParent<Obstruction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Ladder>(out Ladder ladder))
        {
            if(parent is Follow)
            {
                Follow follow = parent as Follow;
                if (follow.isClimbing)
                {
                    follow.isClimbing = false;
                    follow.previousLadder = ladder.gameObject;
                    follow.currentLadder = null;
                }
                else if(ladder == follow.currentLadder)
                {
                    follow.isClimbing = true;
                    follow.accessFromTop = ladder.IsAcessFromTop(follow.gameObject);
                }
            }

        }
    }
}
