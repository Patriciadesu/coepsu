using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : Obstruction
{
    public bool isGround;
    public float gravity = 25.0f;
    public float jumpForce;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;

    public override void Start()
    {
        
        characterController = this.GetComponent<CharacterController>();
    }

    public override void Move()
    {
        Vector3 right = transform.TransformDirection(Vector3.right);
        float moveSpeed =  speed * direction;
        float movementDirectionY = moveDirection.y;
        moveDirection = (right * moveSpeed);
        Debug.Log(moveDirection.x);
        moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0);

        if (characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

}
