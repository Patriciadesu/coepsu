using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hammer", menuName = "ItemData/Hammer")]
public class Hammer : Item
{
    public override void Interact(GameObject interact)
    {
        if (interact.TryGetComponent<Obstruction>(out Obstruction obstruction))
        {
            obstruction.SelfDestruct();
        }
    }
}
