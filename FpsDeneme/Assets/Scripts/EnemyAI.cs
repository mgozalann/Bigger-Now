using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 25f;

    NavMeshAgent navMashAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    void Start()
    {
        navMashAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget < chaseRange)
        {
            isProvoked = true;
        }

    }

    private void EngageTarget()
    {
        if (distanceToTarget > navMashAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMashAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log(name + "has seek and is destroying" + target.name );
    }

    private void ChaseTarget()
    {
        navMashAgent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
