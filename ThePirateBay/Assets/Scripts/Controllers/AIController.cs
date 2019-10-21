using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public float chaseDistance = 5f;
    public float suspicionTime = 2f;
    public float waypointDwellTime = 2f;
    public WaypointPath path;

    float waypointTolerance = 0.5f;
    Health health;
    Fighter fighter;
    Mover mover;
    Vector3 targetMovePosition;
    int currentWaypointIndex = 0;

    float currentDwellTime = Mathf.Infinity;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        targetMovePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.isDead) return;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (IsInAttackRange(player) && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior(player);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }
        }

        currentDwellTime += Time.deltaTime;
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void GuardBehavior()
    {
        if (path != null) {
            if (AtWaypoint()) {
                CycleWaypoint();
            }
            targetMovePosition = GetCurrentWaypoint();
        }
        if (currentDwellTime > waypointDwellTime)
        {
            currentDwellTime = 0;
            mover.StartMoveAction(targetMovePosition);
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        waypointDwellTime = UnityEngine.Random.Range(0.5f, 3f);
        currentWaypointIndex = path.GetNextIndex(currentWaypointIndex);
    }
    private Vector3 GetCurrentWaypoint()
    {
        return path.GetWaypoint(currentWaypointIndex);
    }

    private void SuspicionBehavior()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void AttackBehavior(GameObject player)
    {
        fighter.Attack(player);
    }

    private bool IsInAttackRange(GameObject player)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= chaseDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
