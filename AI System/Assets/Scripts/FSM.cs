using UnityEngine;
using UnityEngine.AI;

public enum State
{
    pos1,
    change_color1,
    pos2,
    change_color2,
}

public class FSM : MonoBehaviour
{
    [SerializeField] private InputCommandObject inputCommandObject;
    [SerializeField] private State initialState;
    private State curentState;

    [SerializeField] private Transform pos1, pos2;


    [SerializeField] private NavMeshAgent navMeshAgent;

    private bool newState = false;

    private void Awake()
    {
        curentState = initialState;
    }
    private void Update()
    {
        Debug.Log(curentState);
        switch (curentState)
        {
            
            case State.pos1:
                inputCommandObject.Destination = pos1.position;
                Debug.Log(navMeshAgent.destination);
                if (navMeshAgent.remainingDistance < 1)
                    curentState = State.change_color1;
                break;
            case State.change_color1:
                Color color = new Color(Random.value, Random.value, Random.value);
                inputCommandObject.DesiredColor = color;
                curentState = State.pos2;
                break;
            case State.change_color2:
                inputCommandObject.Destination = pos2.position;
                break;
            case State.pos2:
                inputCommandObject.Destination = pos2.position;
                if (navMeshAgent.remainingDistance < navMeshAgent.radius)
                    curentState = State.change_color2;
                curentState = State.pos1;
                break;
        }
    }
}