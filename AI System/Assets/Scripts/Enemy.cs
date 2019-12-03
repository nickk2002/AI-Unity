using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private bool drawGizmos = false;

    /// Scriptale objects
    private EnemyController enemyController;

    public string State;
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

    public Animator animator;
    
    /// pozitii patrol
    private void SetPatrolPositions()
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
    /// lumina
    private void SetLight()
    {
        /// legat de lumina
        light = transform.GetChild(0).GetComponent<VLight>();
        if (light != null)
        {
            light.spotRange = distanceView;
            light.spotAngle = angle * 2;
        }
        else
            Debug.LogWarning("No light setup");
    }
    /// initializez scriptableobject
    private void SetAiState()
    {
        playerState.curentHealth = playerState.maxHealth;
        aiState.numberAlerted = 0;
        aiState.lastSeendPlayer = Vector3.zero;
        aiState.alarmTriggered = false;
    }
   
    public void UpdateLight(Color color)
    {
        if(light != null)
            light.colorTint = color;
    }



    /// despre inamic -> jucator
    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position);
    }
    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }
    public void TriggerAlarm()
    {
        aiState.alarmTriggered = true;
    }
    public void SetLastSeenPlayer(Vector3 position)
    {
        aiState.lastSeendPlayer = position;
    }
    public Vector3 GetLastSeenPlayer()
    {
        return aiState.lastSeendPlayer;
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
                RaycastHit hit;
                if (Physics.Linecast(transform.position, targetPlayer.transform.position, out hit))
                {
                    GameObject objectHit = hit.collider.gameObject;
                    if (objectHit == Player.Instance)
                        return true;
                }
            }
        }
        return false;
    }

    public void ChangeState(State<Enemy> newState)
    {
        stateMachine.ChangeState(newState);
    }
    public State<Enemy> GetParticularState(Type stateType)
    {
        return stateMachine.GetStateDictionary(stateType);
    }

    // Start is called before the first frame update
    void Start()
    {

        UnityEngine.Random.seed = DateTime.Now.Millisecond;
        if (targetPlayer == null)
            targetPlayer = Player.Instance;

        SetPatrolPositions();
        SetLight();
        SetAiState();

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        var states = new Dictionary<Type, State<Enemy>>
        {
            {typeof(Patrol), new Patrol() },
            {typeof(SwitchColor), new SwitchColor() },
            {typeof(Alarm),new Alarm() },
        };
        stateMachine = new StateMachine<Enemy>(this,states);
        stateMachine.SetCurentState(states[typeof(Patrol)]);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
        Debug.Log(this.gameObject + " is currently in the state : " + stateMachine.GetCurentState());
    }
    void Draw()
    {
        if (navMeshAgent == null)
            return;
        Vector3 destination = navMeshAgent.destination;
        if (destination != null)
        {
            float dist = distanceView;
            Vector3 direction = transform.forward;
            direction = Quaternion.Euler(0, -angle, 0) * direction * dist;
            Vector3 posLeft = transform.position + direction;
            direction = transform.forward;
            direction = Quaternion.Euler(0, angle, 0) * direction * dist;
            Vector3 posRight = transform.position + direction;

            //Debug.DrawLine(transform.position, targetPlayer.transform.position,Color.blue);
            Gizmos.DrawLine(transform.position, posLeft);
            Gizmos.DrawLine(transform.position, posRight);
            Gizmos.DrawLine(posLeft, posRight);
            Gizmos.DrawSphere(destination, 0.5f);
            Gizmos.DrawLine(transform.position, destination);
        }
    }
    private void OnDrawGizmos()
    {
        if(drawGizmos)
            Draw();
    }
}
