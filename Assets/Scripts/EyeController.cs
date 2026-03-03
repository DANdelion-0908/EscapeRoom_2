using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EyeController : MonoBehaviour
{
    [SerializeField] Transform objective;
    [SerializeField] Animator animator;
    private NavMeshAgent Agent => GetComponent<NavMeshAgent>();

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(objective.position);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
