using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SlimeController : MonoBehaviour
{
    private enum EnemyState { Idle, Flee }

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float waitTime = 3.0f;
    [SerializeField] Animator animator;
    private int wpIndex = 0;
    private  EnemyState currentState = EnemyState.Flee;
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
            case EnemyState.Flee:
                Flee();
                break;

            default:
                break;
        }

        animator.SetFloat("Speed", Agent.velocity.magnitude);
        Debug.Log(currentState.ToString());
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<LevelManager>().IncreaseCoinScore();
            Destroy(gameObject);
        }
    }

    private void Flee()
    {
        Agent.speed = 5.0f;

        if (Agent.remainingDistance < 0.5f && !Agent.isStopped) 
        {
            StartCoroutine(FleePoint());
        }
    }

    IEnumerator FleePoint()
    {
        Agent.isStopped = true;
        yield return new WaitForSeconds(waitTime);
        wpIndex = Random.Range(0, waypoints.Count);
        Agent.SetDestination(waypoints[wpIndex].transform.position);
        Agent.isStopped = false;
    }
}
