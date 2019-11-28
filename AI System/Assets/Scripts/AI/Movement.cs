using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour
{

    [SerializeField] public EnemyState enemyState;
    [SerializeField] private AIState aiState;
    [SerializeField] private LayerMask viewMask;

    [SerializeField] private float alarmSpeed;
    private float angle;
    private float distance;
    

    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private EnemyController enemyController;

    bool CanSeePlayer()
    {
        
        player = GameController.Player; 
        if(Vector3.Distance(transform.position,player.transform.position) < distance)
        {
            
            Vector3 directionPlayer = (player.transform.position - transform.position).normalized;
            float angleEnemyPlayer = Vector3.Angle(transform.forward, directionPlayer);
            if (angleEnemyPlayer <= angle)
            {
                if (!Physics.Linecast(transform.position, player.transform.position, viewMask))
                    return true;
            }
        }
        return false;
    }
    void AlarmTriggered()
    {
        Debug.Log("DA");
        navMeshAgent.speed = alarmSpeed;
    }

    private void Awake()
    {
        GameObject parinte = transform.parent.gameObject;
        enemyController = parinte.GetComponent<EnemyController>();

        angle = enemyController.angle;
        distance = enemyController.distance;

        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyState = enemyController.inputCommandObject;
        aiState.alarmEvent.AddListener(AlarmTriggered);
    }
    void Update()
    {
        navMeshAgent.destination = enemyState.destination;
        if (CanSeePlayer())
        {
            aiState.alarmEvent.Invoke();
        }
    }
    private void OnDrawGizmos()
    {
        
        Vector3 destination = Vector3.zero;
        if (enemyState != null)
        {
            destination = enemyState.destination;
        }
        if (destination != null)
        {

            Vector3 direction = transform.forward;
            direction = Quaternion.Euler(0, -angle, 0) * direction * distance;            
            Vector3 posLeft = transform.position + direction;
            direction = transform.forward;
            direction = Quaternion.Euler(0, angle, 0) * direction * distance;
            Vector3 posRight = transform.position + direction;

            Gizmos.DrawLine(transform.position, posLeft);
            Gizmos.DrawLine(transform.position, posRight);
            Gizmos.DrawLine(posLeft, posRight);
            Gizmos.DrawSphere(destination, 0.5f);
            Gizmos.DrawLine(transform.position, destination);
        }
        

    }
}
