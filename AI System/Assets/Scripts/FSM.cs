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

    private Vector3 playerPosition;

    private void Awake()
    {
        curentState = initialState;
    }
    private void Update()
    {
        Debug.Log(curentState);

        playerPosition = navMeshAgent.gameObject.transform.position;

        switch (curentState)
        {
            case State.pos1:
                if (!navMeshAgent.pathPending)
                {
                    inputCommandObject.Destination = pos1.position;
                   
                }
                if (Vector3.Distance(playerPosition,pos1.position) < 2f)
                {
                    Debug.Log(navMeshAgent.destination);
                    Debug.Log(navMeshAgent.gameObject.transform.position);
                    Debug.Log("Am ajuns");
                    curentState = State.change_color1;
                }
                break;
            case State.change_color1:
                Color color = new Color(Random.value, Random.value, Random.value);
                inputCommandObject.DesiredColor = color;
                curentState = State.pos2;
                break;
            case State.change_color2:
                color = new Color(Random.value, Random.value, Random.value);
                inputCommandObject.DesiredColor = color;
                curentState = State.pos1;
                break;
            case State.pos2:
                if (!navMeshAgent.pathPending)
                {
                    inputCommandObject.Destination = pos2.position;

                }
                if (Vector3.Distance(playerPosition, pos2.position) < 2f)
                {
                    Debug.Log("Am ajuns");
                    curentState = State.change_color2;
                }
                break;
        }
    }
}