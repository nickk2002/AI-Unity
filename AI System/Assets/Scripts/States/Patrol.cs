using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State<Enemy> 
{
    private Transform [] patrolPositions;
    private int index = 0;
    private int numberPositions = 0;
    private Enemy enemy;
    public Patrol()
    {
        patrolPositions = new Transform[] { };
    }
    public override void Enter(Enemy owner)
    {
        enemy = owner;
        patrolPositions = owner.patrolPositions;
        numberPositions = patrolPositions.Length;
    }
    public override void Execute(Enemy owner)
    {
        Vector3 destination = patrolPositions[index].position;
        /// owner.NavMeshAgent.transform.position -.> (10,0,10) , (0,0,0)
        if (owner.transform.position != destination)
            owner.SetDestination(destination);
        if (Vector3.Distance(owner.transform.position, destination) < 2f)
        {
            index++;
            index = index % numberPositions;
            owner.ChangeState(owner.GetParticularState(typeof(SwitchColor)));
        }
        if (owner.CanSeePlayer())
        {
            if (owner.aiState.alarmTriggered == false)
            {
                owner.TriggerAlarm();
                owner.aiState.alarmTriggered = true;
            }
            owner.SetLastSeenPlayer(Player.GameobjectInstance.transform.position);
        }
        if (owner.aiState.alarmTriggered == true && owner.aiState.numberAlerted  + 1  <= owner.aiState.maxNumberAlerted)
        {
            Debug.Log(enemy);
            owner.aiState.numberAlerted++;
            owner.ChangeState(owner.GetParticularState(typeof(Alarm)));
        }
            
    }
    public override void Exit(Enemy owner)
    {
       
    }
}
