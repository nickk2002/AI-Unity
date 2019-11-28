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
    private static Patrol instance = null;
    private static readonly object padlock = new object();

    public Patrol()
    {
        patrolPositions = new Transform[] { };
        
    }
    public static Patrol Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Patrol();
                }
                return instance;
            }
        }
    }
    public override void Enter(Enemy owner)
    {
        patrolPositions = owner.patrolPositions;
        numberPositions = patrolPositions.Length;
    }
    public override void Execute(Enemy owner)
    { 
        Vector3 destination = patrolPositions[index].position;
        if(owner.NavMeshAgent.transform.position != destination)
            owner.SetDestination(destination);
        if (Vector3.Distance(owner.NavMeshAgent.transform.position, patrolPositions[index].position) < 2f)
        {
            index++;
            index = index % numberPositions;
            owner.ChangeState(SwitchColor.Instance);
        }
        if (owner.CanSeePlayer())
        { 
            owner.aiState.alarm = true;
            owner.aiState.lastSeendPlayer = owner.targetPlayer.transform.position;
        }
        if (owner.aiState.alarm == true)
            owner.ChangeState(Alarm.Instance);
            
    }
    public override void Exit(Enemy owner)
    {
       
    }
}
