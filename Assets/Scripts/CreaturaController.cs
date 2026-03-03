using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class CreaturaController : MonoBehaviour
{
    private enum EnemyState { Idle, Patrol, Chase, Attack }

    [Serialized Field] Transform objective;
    [Serialized Field] List<Transform> waypoints;
    [Serialized Field] float waitTime = 3.0f;

    private float viewRadius = 10.0f;
    private float viewAngle = 90.0f;

    private  EnemyState currentState = EnemyState.Patrol;

    private NavMeshAgent agent => GetComponent<NavMeshAgent>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wpIndex = Random.Range(0, waypoints.Count);
        agent.setDestination(waypoints[wpIndex].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) 
        {
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Chase:
                Chase();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Idle:
                Idle();
                break;
        }
    }

    private void Patrol() 
    {
        if (agent.remainingDistance < 0.5f && !agent.isStopped) 
        {
            StartCoroutine(PatrolPoint());
        }

        if (LookForObjective())
        {
            currentState = EnemyState.Chase;
        }
    }

    private void Chase()
    {
        agent.SetDestination(objective.position);
    }

    private void Attack()
    {

    }

    private void Idle()
    {

    }




    private bool LookForObjective()
    {
        if (objective == null)
        {
            return false;
        }

        Vector3 dir = objective.position - transform.position;
        if (dir.magnitude > viewRadius)
        {
            return false;
        }

        float angleToObjective = Vector3.Angle(transform.forward, dir.normalized);
        if (angleToObjective > viewAngle / 2.0f)
        {
            return false;
        }

        if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, out Raycast hit, viewRadius))
        {
            if (hit.transform == objective)
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator PatrolPoint()
    {
        agent.isStopped = true;

        yield return new WaitForSeconds(waitTime);
    }
}
