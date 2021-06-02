using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAi : MonoBehaviour
{
    [Header("General AI Settings")]

    // Set from inspector to indicate which animations should play.
    [SerializeField] private bool rat, wolf;
    // Set to the player to indicate when the animal has the player in sight range; animal will then run to the moveToThisDestination.
    [SerializeField] private LayerMask selectTargetLayer;
    [SerializeField] private Transform moveToThisDestination;
    [SerializeField] private float sightRange;
    private bool TargetInSightRange => Physics.CheckSphere(transform.position, sightRange, selectTargetLayer);
    
    [Header("Patrol Settings")]

    [SerializeField] private bool patrolWaiting; // Set from inspector
    [SerializeField] private float totalWaitTime;
    [SerializeField] private List<Waypoint> patrolPoints; // reference to Waypoint class
    private NavMeshAgent agentIfNPCMoves;
    private bool NpcHasAgentAndPatrolPoints => agentIfNPCMoves != null && patrolPoints != null && patrolPoints.Count >= 2;
    private int currentPatrolIndex;
    private bool traveling;
    private bool waiting;
    private float waitTimer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agentIfNPCMoves = GetComponent<NavMeshAgent>();
        CheckForPatrolPoints();
    }

    private void Update()
    {
        AiMoveToDestinationOrPatrol();
        CheckForDestinationDistance();
        CheckForPatrolWaiting();
    }

    private void AiMoveToDestinationOrPatrol()
    {
        if (TargetInSightRange)
        {
            SetMoveDestination();
        }
        else
        {
            AiPatrol();
        }
    }

    private void AiPatrol()
    {
        if (!TargetInSightRange)
        {
            CheckForDestinationDistance();
            CheckForPatrolWaiting();
        }
    }

    private void CheckForPatrolPoints()
    {
        if (NpcHasAgentAndPatrolPoints)
        {
            currentPatrolIndex = 0;
            SetPatrolDestination();
        }
        else
        {
            throw new Exception("You need at least two patrol points in scene & a Navmesh Agent to move.");
        }
    }

    private void CheckForDestinationDistance()
    {
        if (traveling && agentIfNPCMoves.remainingDistance <= 0.1f) // NOTE: adjustment here depending on AI behavior 
        {
            traveling = false;

            if (patrolWaiting)
            {
                waiting = true; // OR set idle 
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }
    }

    private void CheckForPatrolWaiting()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetPatrolDestination();
            }
        }
    }

    private void SetPatrolDestination()
    {
        if (patrolPoints != null)
        {
            if (rat)
            {
                anim.SetBool("ratIsRunning", true);
            }
            else if (wolf)
            {
                anim.SetBool("isWalking", true);
            }
            
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            agentIfNPCMoves.SetDestination(targetVector);
            traveling = true;
        }
    }

    private void ChangePatrolPoint()
    {
        currentPatrolIndex++;

        if (currentPatrolIndex >= patrolPoints.Count)
        {
            currentPatrolIndex = 0;
        }
    }

    private void SetMoveDestination()
    {
        if (moveToThisDestination != null)
        {
            Vector3 targetVector = moveToThisDestination.transform.position;
            agentIfNPCMoves.SetDestination(targetVector);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}