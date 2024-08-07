using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public GameObject prefab;

    public bool canJump;
    public bool canCrouch;
    public bool canClimb;

    public virtual void Interact(GameObject interact)
    {

    }

}
