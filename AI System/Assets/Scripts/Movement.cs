using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{

    [SerializeField] private InputCommandObject InputCommandObject;
    private NavMeshAgent navMeshAgent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        navMeshAgent.destination = InputCommandObject.Destination;
    }
    private void OnDrawGizmos()
    {
        Vector3 destination = InputCommandObject.Destination;
        if (destination != null)
        {
            Gizmos.DrawSphere(destination, 0.5f);
            Gizmos.DrawLine(transform.position, destination);
        }

    }
}
