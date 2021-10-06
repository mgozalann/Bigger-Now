using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonAI : MonoBehaviour
{
    float chaseRange = 25f;
    Animator animator;

    NavMeshAgent navMashAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    void Start()
    {
        navMashAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = GetTarget();
        if (target != null) {
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (isProvoked)
            {
                EngageTarget(target);
            }
            else if (distanceToTarget < chaseRange)
            {
                isProvoked = true;
            }
        }
        Debug.Log(target.name);
    }

    private void EngageTarget(Transform target)
    {
        if (distanceToTarget > navMashAgent.stoppingDistance)
        {
            ChaseTarget(target);
            animator.SetBool("isClose", false);
        }
        else if (distanceToTarget <= navMashAgent.stoppingDistance)
        {
            animator.SetBool("isClose", true);
        }
    }
    private void ChaseTarget(Transform target)
    {
        navMashAgent.SetDestination(target.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    private Transform GetTarget ()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach (var enemies in colliders)
        {
            Enemy enemy = enemies.GetComponent<Enemy>();
            if (enemy != null)
            {
                return enemy.transform;
            }  
        }
        return null;
    }
}
