using UnityEngine;
using UnityEngine.AI;

public enum State
{
    patrol,
    change_color
}

public class FSM : MonoBehaviour
{
    [SerializeField] private InputCommandObject inputCommandObject;
    [SerializeField] private State initialState;
    private State curentState;

    [SerializeField] private Transform [] Positions;


    [SerializeField] private NavMeshAgent navMeshAgent;

    private int index = 0;
    private int numberPositions;
    private Vector3 agentPosition,destinationPosition;


    private void Awake()
    {
        curentState = initialState;
        numberPositions = Positions.Length;
    }
    private void Update()
    {
        Debug.Log(numberPositions);

        agentPosition = navMeshAgent.gameObject.transform.position;
        Debug.Log(index);
        destinationPosition = Positions[index].position;
        switch (curentState)
        {
            case State.patrol:
                if (!navMeshAgent.pathPending)
                {
                    inputCommandObject.Destination = destinationPosition;
                }
                if (Vector3.Distance(agentPosition, destinationPosition) < 2f)
                {
                    Debug.Log("Am ajuns in pozitia : " + index + 1 + ":" + destinationPosition);
                    curentState = State.change_color;
                }
                break;
            case State.change_color:
                Color color = new Color(Random.value, Random.value, Random.value);
                inputCommandObject.DesiredColor = color;
                curentState = State.patrol;
                index++;
                index = index % numberPositions;
                break;
            
        }
    }
}