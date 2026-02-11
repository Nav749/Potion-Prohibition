using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Chase,
    Attack,
}

public enum Transitions
{
    SeePlayer,
    LostPlayer,
    ReachesPlayer,
}

public class NavMeshEnemyTest : MonoBehaviour
{
    public State CurrentState = State.Idle;
    public Transform player;

    [SerializeField] private float chaseDistance = 10f;
    [SerializeField] private float attackDistance = 1.0f;

    private void Update()
    {
        State nextState = CurrentState;
        switch (CurrentState)
        {
            case State.Idle:
                nextState = runIdleState();
                break;
            case State.Chase:
                nextState = runChaseState();
                break;
            case State.Attack:
                nextState = runAttackState();
                break;
        }

        if (nextState != CurrentState)
        {
            CurrentState = nextState;
        }
    }
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();

    private int currentPatrolPoint = 0;

    [SerializeField] private float movementSpeed = 5f;
    State runIdleState()
    {
        Transform target = patrolPoints[currentPatrolPoint];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            currentPatrolPoint = Random.Range(0, patrolPoints.Count);
        }

        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
            return State.Chase;
        }


        return State.Idle;
    }
    State runChaseState()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;

        if (DistanceCheck(attackDistance))
        {
            return State.Attack;
        }

        if (DistanceCheck(chaseDistance))
        {
            return State.Idle;
        }

        return State.Chase;
    }

    State runAttackState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            return State.Chase;
        }
        return State.Attack;
    }

    bool DistanceCheck(float distanceToCheck)
    {
        if (!player) return false;
        return Vector3.Distance(transform.position, player.position) <= distanceToCheck;
    }

}
