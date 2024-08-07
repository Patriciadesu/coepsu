using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Follow : Obstruction
{
    public float climbSpeed = 5f;
    public GameObject currentLadder;
    public GameObject previousLadder;
    public GameObject[] unableToGo = { };
    public CharacterController characterController;
    public bool isClimbing = false;
    public bool accessFromTop;

    public override void Start()
    {
        base.Start();
        characterController = this.GetComponent<CharacterController>();
    }

    bool CheckIfSameFloor(GameObject target)
    {
        float currentFloor = this.transform.position.y;
        float targetFloor = target.transform.position.y;
        if(target.TryGetComponent<Ladder>(out Ladder ladder))
        {
            if(ladder.IsAcessFromTop(this.gameObject))targetFloor += target.transform.lossyScale.y / 2;
            else targetFloor -= target.transform.lossyScale.y / 2;
        }
        else if (target.TryGetComponent<Player>(out Player player))
        {
            targetFloor = FindObjectOfType<GroundCheck>().transform.position.y;
        }
        Debug.Log("Target's lossy" + targetFloor);
        float distance = Mathf.Abs(targetFloor - currentFloor);
        return distance < floorInterval;
    }

    GameObject FindNearestLadder()
    {
        Ladder[] ladders = FindObjectsOfType<Ladder>();
        float nearestDistance = Mathf.Infinity;
        GameObject nearestLadder = null;

        foreach (Ladder ladder in ladders)
        {
            float distance = (ladder.transform.position - this.transform.position).magnitude;
            if (!unableToGo.Contains(ladder.gameObject) && previousLadder != ladder.gameObject && distance < nearestDistance && CheckIfSameFloor(ladder.gameObject))
            {
                Debug.Log("Find Nearest Ladder Candidate");
                nearestDistance = distance;
                nearestLadder = ladder.gameObject;
            }
        }

        if (nearestLadder != null)
        {
            Debug.Log("Return LADDER");
            return nearestLadder;
        }
        else if (previousLadder != null)
        {
            Debug.Log("Return Previous Ladder");
            return previousLadder;
        }
        else
        {
            return null;
        }
    }

    void MoveToTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized * speed;
        direction.y = rigidbody.velocity.y;
        characterController.Move(direction * Time.deltaTime);
    }

    public override void Move()
    {
        if (!isClimbing)
        {
            if (CheckIfSameFloor(Player.Instance.gameObject))
            {
                Debug.Log("Same Floor");
                MoveToTarget(Player.Instance.gameObject);
            }
            else
            {
                Debug.Log("NOT Same Floor");
                currentLadder = FindNearestLadder();
                if (currentLadder == null)
                {
                    Debug.Log("No ladder found or previous ladder is the only option");
                }
                else
                {
                    Debug.Log("Moving To Ladder");
                    MoveToTarget(currentLadder);
                    if ((currentLadder.transform.position - this.transform.position).magnitude < 2)
                    {
                        isClimbing = true;
                    }
                }
            }
        }
        if (isClimbing)
        {
            characterController.Move(Vector3.zero); // Stop horizontal movement when climbing
            int direction = accessFromTop ? -1 : 1;
            this.transform.position += new Vector3(0, climbSpeed * Time.deltaTime * direction, 0);

            if (Mathf.Abs(this.transform.position.y - currentLadder.transform.position.y) > currentLadder.transform.lossyScale.y / 2)
            {
                isClimbing = false; // Stop climbing when reaching the top/bottom of the ladder
            }
        }
    }
}
