using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstruction : MonoBehaviour
{
    [HideInInspector] public bool isDestructing = false;
    [HideInInspector] public int direction = 1;

    public float speed;

    [SerializeField]protected float floorInterval;

    protected Animator animator;
    protected Rigidbody rigidbody;

    public virtual void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
    }
    public virtual void Update()
    {
        Move();
    }
    public virtual void FixUpdate()
    {
        
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("Change Direction");
            direction *= -1;
        }
        OnTouch();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    public virtual void Move()
    {

    }
    public virtual void OnTouch()
    {
        
    }

    public void SelfDestruct()
    {
        Destroy(this.gameObject);
    }
    IEnumerator Destructing()
    {
        isDestructing = true;
        animator.SetTrigger("selfDestruct");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(this.gameObject);
    }

}
