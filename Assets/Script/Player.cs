using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float speed;
    [SerializeField] float climbSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float unequipForce=8;
    public Item currentItem;
    public Item CurrentItem
    {
        get
        {
            return currentItem;
        }
        set
        {
            if (value == null) currentItem = noItem;
            else currentItem = value;
            canClimb = value.canClimb;
            canJump = value.canJump;
            canCrouch = value.canCrouch;
        }
    }
    [SerializeField] Item noItem;

    public float externalForce;
    public bool canJump = true;
    public bool canClimb = true;
    public bool canCrouch = true;
    public bool isClimbing = false;
    public bool isLadderInFront = false;
    public Rigidbody rigidbody;
    public Animator animator;

    #region MovementProperty
    public bool isGround;
    public float gravity = 20.0f;
    public Camera playerCamera;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;
    #endregion

    void Start()
    {
        currentItem = noItem;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UnEquipItem();
        }
        if (Input.GetKeyDown(KeyCode.F)&& canClimb && isLadderInFront)
        {
            isClimbing = true;
            EnterClimbState();
        }

        if (isClimbing)
        {
            Climb();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ItemHolder>(out ItemHolder itemHolder))
        {
            EquipItem(itemHolder);
        }
        if (other.gameObject.tag == "win")
        {
            GameManager.Instance.NextLevel();
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<ItemHolder>(out ItemHolder itemHolder))
        {
            EquipItem(itemHolder);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        currentItem.Interact(collision.gameObject);
        
    }

    public void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (canMove) {
            
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            moveDirection = new Vector3(moveDirection.x + externalForce, moveDirection.y, moveDirection.z);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded && canJump)
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            if (!isGround)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
    
    public void Crouch()
    {
        if (canCrouch)
        {

        }
    }
    public void Climb()
    {
        if (canClimb)
        {
            float direction = Input.GetAxis("Vertical");
            float yAxis = this.transform.position.y;
            this.transform.position = new Vector3(this.transform.position.x, yAxis + (climbSpeed * Time.deltaTime * direction), this.transform.position.z);
        }
    }

    public void EquipItem(ItemHolder itemHolder)
    {
        Item item = itemHolder.item;
        if (CurrentItem == noItem && item!=null)
        {
            Destroy(itemHolder.gameObject);
            CurrentItem = item;
        }
    }
    public void UnEquipItem()
    {
        if (CurrentItem != noItem)
        {
            Rigidbody item = Instantiate(CurrentItem.prefab, this.transform.position+(transform.up*2), Quaternion.identity).AddComponent<Rigidbody>();
            item.AddForce(item.gameObject.transform.up * unequipForce, ForceMode.Impulse);
            CurrentItem = noItem;
        }
    }
    public void Die()
    {
        StartCoroutine(Dying());
    }
    private IEnumerator Dying()
    {
        animator.SetTrigger("isDying");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameManager.Instance.ResetLevel();
    }


    public void EnterClimbState()
    {
        canMove = false;
        canClimb = true;
        canCrouch = false;
        canJump = false;
    }
    public void ExitClimbState()
    {
        EnableAction();
    }
    public void DisableAction()
    {
        canMove = false;
        canClimb = false;
        canCrouch = false;
        canJump = false;
    }
    public void EnableAction()
    {
        canMove = true;
        canClimb = true;
        canCrouch = true;
        canJump = true;
    }
    public void SetAction(bool value)
    {
        canMove = value;
        canClimb = value;
        canCrouch = value;
        canJump = value;
    }
}
