using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
 
    /// Scriptale objects
    private EnemyController enemyController;


    /// initial state
    [SerializeField] private _State initialState;
    private StateMachine<Enemy> stateMachine;

    [SerializeField] public float damage;
    [SerializeField] private float distanceView;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask viewMask;

    [SerializeField] public Transform[] patrolPositions;
    [SerializeField] private GameObject objectWithChildren;

    /// legat de navmesh
    private NavMeshAgent navMeshAgent;
    private MeshRenderer meshRenderer;
    private VLight light;

    [SerializeField] public AIState aiState;
    [SerializeField] public PlayerState playerState;

    public NavMeshAgent NavMeshAgent
    {
        get => navMeshAgent;
        set => navMeshAgent = value;
    }
    [SerializeField] public GameObject targetPlayer;
    

    void SetPatrolPositions()
    {
        
        if (objectWithChildren != null && patrolPositions.Length == 0)
        {
            int i = 0;
            patrolPositions = new Transform[objectWithChildren.transform.childCount];
            foreach (Transform child in objectWithChildren.transform)
            {
                //Debug.Log(this.gameObject + " " + child.position);
                patrolPositions[i++] = child;
            }
        }
    }

    public void UpdateLight(Color color)
    {
        light.colorTint = color;
    }
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position);
    }
    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

    public void ChangeState(State<Enemy> newState)
    {
        stateMachine.ChangeState(newState);
    }
    
    public bool CanSeePlayer()
    {
        float curentDist = DistanceToPlayer();
        Vector3 directionPlayer = (targetPlayer.transform.position - transform.position).normalized;
        float angleEnemyPlayer = Vector3.Angle(transform.forward, directionPlayer);
        if (curentDist <= distanceView)
        {
            if (angleEnemyPlayer <= angle)
            {
                if (!Physics.Linecast(transform.position, targetPlayer.transform.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private float AreaCalculator(float xa,float ya,float xb,float yb,float xc,float yc)
    {
        float modul = Mathf.Abs(xa * (yb - yc) + xb * (yc - ya) + xc * (ya - yb));
        return modul / 2;
    }
    void SetLight()
    {
        /// legat de lumina
        light = transform.GetChild(0).GetComponent<VLight>();
        light.spotRange = distanceView * Mathf.Cos((Mathf.PI * angle / 180));
        light.spotAngle = angle * 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (targetPlayer == null)
            targetPlayer = Player.Instance;
        SetPatrolPositions();
        SetLight();

        aiState.lastSeendPlayer = Vector3.zero;
        aiState.alarm = false;

        navMeshAgent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();


        stateMachine = new StateMachine<Enemy>(this);
        stateMachine.SetCurentState(new Patrol());
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        if (CanSeePlayer())
            Debug.Log("see player");
        else
            Debug.Log("Can't see player");
    }
    void Draw()
    {
        if (navMeshAgent == null)
            return;
        Vector3 destination = navMeshAgent.destination;
        if (destination != null)
        {
            Vector3 direction = transform.forward;
            direction = Quaternion.Euler(0, -angle, 0) * direction * distanceView;
            Vector3 posLeft = transform.position + direction;
            direction = transform.forward;
            direction = Quaternion.Euler(0, angle, 0) * direction * distanceView;
            Vector3 posRight = transform.position + direction;

            Gizmos.DrawLine(transform.position, posLeft);
            Gizmos.DrawLine(transform.position, posRight);
            Gizmos.DrawLine(posLeft, posRight);
            Gizmos.DrawSphere(destination, 0.5f);
            Gizmos.DrawLine(transform.position, destination);
        }
    }
    private void OnDrawGizmos()
    {
        Draw();
    }
}
