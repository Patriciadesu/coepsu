using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="groundCheck")Destroy(this.gameObject, 1);
    }
}
