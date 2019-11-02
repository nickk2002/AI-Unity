using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private NavMeshAgent navMeshAgent;


    private void OnDrawGizmos()
    {
        if (destination)
        {
            Gizmos.DrawSphere(destination.position, 0.5f);
            Gizmos.DrawLine(transform.position, destination.position);
        }

    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(destination.position);
    }
}
