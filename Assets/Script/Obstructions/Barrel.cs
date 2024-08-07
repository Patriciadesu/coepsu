using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Obstruction
{
    public Mesh prefabMesh;

    public override void Start()
    {
        base.Start();
        this.GetComponent<MeshFilter>().sharedMesh = prefabMesh;
    }

    public override void Move()
    {
        rigidbody.velocity = new Vector3(speed * direction, rigidbody.velocity.y, rigidbody.velocity.z);
    }
}
