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

    public float ladderReachThreshold = 0.5f; // Tolerance for reaching the ladder

    public override void Start()
    {
        base.Start();
        characterController = this.GetComponent<CharacterController>();
    }

    bool CheckIfSameFloor(GameObject target)
    {
        float currentFloor = this.transform.position.y;
        float targetFloor = target.transform.position.y;
        if (target.TryGetComponent<Ladder>(out Ladder ladder))
        {
            if (ladder.IsAcessFromTop(this.gameObject))
                targetFloor += target.transform.lossyScale.y / 2;
            else
                targetFloor -= target.transform.lossyScale.y / 2;
        }
        else if (target.TryGetComponent<Player>(out Player player))
        {
            targetFloor = FindObjectOfType<GroundCheck>().transform.position.y;
        }
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
                nearestDistance = distance;
                nearestLadder = ladder.gameObject;
            }
        }

        return nearestLadder != null ? nearestLadder : previousLadder;
    }

    bool MoveToTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - this.transform.position).normalized * speed;
        direction.y = rigidbody.velocity.y;
        characterController.Move(direction * Time.deltaTime);

        // Check if the object is within the ladder reach threshold
        return Vector3.Distance(new Vector3(this.transform.position.x, 0, this.transform.position.z),
                                new Vector3(target.transform.position.x, 0, target.transform.position.z)) < ladderReachThreshold;
    }

    public override void Move()
    {
        if (!isClimbing)
        {
            if (CheckIfSameFloor(Player.Instance.gameObject))
            {
                MoveToTarget(Player.Instance.gameObject);
            }
            else
            {
                currentLadder = FindNearestLadder();
                if (currentLadder != null && MoveToTarget(currentLadder))
                {
                    isClimbing = true;
                    previousLadder = currentLadder;
                }
            }
        }

        if (isClimbing)
        {
            Debug.Log("Climbbbbbb");
            characterController.Move(Vector3.zero); // Stop horizontal movement when climbing
            int direction = accessFromTop ? -1 : 1;
            this.transform.position += new Vector3(0, climbSpeed * Time.deltaTime * direction, 0);

        }
    }
}
