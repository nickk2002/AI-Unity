using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{
    [SerializeField] public InputCommandObject inputCommandObject;
    EnemyController enemyController;
    private NavMeshAgent navMeshAgent;
    private void Start()
    {
        GameObject parinte = transform.parent.gameObject;
        enemyController = parinte.GetComponent<EnemyController>();
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        inputCommandObject = enemyController.inputCommandObject;
    }
    void Update()
    {
        navMeshAgent.destination = inputCommandObject.destination;
    }
    private void OnDrawGizmos()
    {
        Vector3 destination = Vector3.zero;
        if (inputCommandObject != null)
        {
            destination = inputCommandObject.destination;
        }
        if (destination != null)
        {
            Gizmos.DrawSphere(destination, 0.5f);
            Gizmos.DrawLine(transform.position, destination);
        }

    }
}
