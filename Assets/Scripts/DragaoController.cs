using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class DragaoController : MonoBehaviour
{
    private enum EnemyState { Fly, Attack }

    [SerializeField] Transform objective;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float waitTime = 10.0f;
    [SerializeField] Animator animator;

    private int wpIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(gameObject.transform.position.x, 60, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0,60,0) * Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
