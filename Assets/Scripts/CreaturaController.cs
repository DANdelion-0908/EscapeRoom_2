using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreaturaController : MonoBehaviour
{
    private enum EnemyState { Idle, Patrol, Chase, Attack }

    [SerializeField] Transform objective;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float waitTime = 3.0f;
    [SerializeField] Animator animator;

    private readonly float viewRadius = 20.0f;
    private readonly float viewAngle = 90.0f;
    private readonly float attackRange = 2.0f;
    private readonly float attackCooldown = 1.0f;
    private float lastAttackTime = 0.0f;
    private readonly float loseSightMaxTime = 5.0f;
    private float loseSightTimer = 0.0f;
    private int wpIndex = 0;
    private  EnemyState currentState = EnemyState.Patrol;
    private NavMeshAgent Agent => GetComponent<NavMeshAgent>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wpIndex = Random.Range(0, waypoints.Count);
        Agent.SetDestination(waypoints[wpIndex].transform.position);
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

            default:
                break;
        }

        animator.SetFloat("Speed", Agent.velocity.magnitude);
        Debug.Log(currentState.ToString());
    }

    private void Patrol()
    {
        animator.SetBool("isChasing", false);
        Agent.speed = 2.0f;

        if (Agent.remainingDistance < 0.5f && !Agent.isStopped) 
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
        animator.SetBool("isChasing", true);
        Agent.speed = 3.5f;
        Agent.SetDestination(objective.position);

        if (Agent.remainingDistance < attackRange)
        {
            currentState = EnemyState.Attack;
        
        } else if (!LookForObjective()){
            loseSightTimer += Time.deltaTime;

            if (loseSightTimer >= loseSightMaxTime)
            {
                loseSightTimer = 0.0f;
                currentState = EnemyState.Patrol;
            }
        }
    }

    private void Attack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            transform.LookAt(objective);
            Agent.stoppingDistance = attackRange;
            Agent.SetDestination(transform.position);

            lastAttackTime = Time.time;

            animator.SetTrigger("Attack");
        
        }
        else 
        {
            float dis = Vector3.Distance(transform.position, objective.position);

            if (dis > attackRange)
            {
                currentState = EnemyState.Chase;
            }
        }
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

        if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, out RaycastHit hit, viewRadius))
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
        Agent.isStopped = true;

        yield return new WaitForSeconds(waitTime);

        int newIndex;

        do
        {
            newIndex = Random.Range(0, waypoints.Count);
        } while (newIndex == wpIndex);

        wpIndex = newIndex;
        Agent.SetDestination(waypoints[wpIndex].transform.position);

        Agent.isStopped = false;
    }
}
