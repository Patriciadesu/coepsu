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
        Debug.Log(direction);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float moveSpeed = speed;

        // Horizontal movement
        moveDirection.x = moveSpeed;

        if (characterController.isGrounded)
        {
            isGround = true;

            // Jumping logic
            if (jumpForce > 0)
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = -1f; // Small downward force to keep grounded
            }
        }
        else
        {
            isGround = false;

            // Apply gravity when not grounded
            moveDirection.y -= gravity * Time.deltaTime;
        }
        if (direction == -1)
        {
            moveDirection.x = Mathf.Abs(moveDirection.x) * -1; // Ensure negative direction
        }
        else
        {
            moveDirection.x = Mathf.Abs(moveDirection.x); // Ensure positive direction
        }
        // Move the character controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
