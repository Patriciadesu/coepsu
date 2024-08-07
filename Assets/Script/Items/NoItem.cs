using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoItem", menuName = "ItemData/NoItem")]
public class NoItem : Item
{
    public override void Interact(GameObject interact)
    {
        if(interact.TryGetComponent<Obstruction>(out Obstruction obstruction))
        {
            if (!obstruction.isDestructing)
            {
                if (obstruction is Trigger)
                {
                    //Trigger obstruction obj
                }
                else
                {
                    Player.Instance.Die();
                }
            }
        }
    }
}
