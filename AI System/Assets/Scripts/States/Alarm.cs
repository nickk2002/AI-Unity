using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alarm : State<Enemy>
{
    private static Alarm instance = null;
    private static object padlock = new object();
    private float radius;
    private float curentTries,numberTries;
    private float shootingDistance;

    private Alarm()
    {

    }
    public static Alarm Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new Alarm();
                return instance;
            }
        }
    }

    public override void Enter(Enemy owner)
    {
        Debug.Log("Entered Alarm!");
        owner.NavMeshAgent.speed = owner.aiState.alarmSpeed;
        radius = owner.aiState.searchRadius;
        shootingDistance = owner.aiState.shootingDistance;
        curentTries = owner.aiState.numberTries;
        owner.SetDestination(owner.aiState.lastSeendPlayer);
        owner.aiState.alarmEvent.Invoke();
        curentTries = 0;
    }

    public override void Execute(Enemy owner)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
        Vector3 randomDestination = randomPosition + owner.aiState.lastSeendPlayer;
        if (owner.CanSeePlayer() && owner.DistanceToPlayer() <= shootingDistance)
            owner.ChangeState(Shoot.Instance);
        if (owner.NavMeshAgent.remainingDistance < 1)
        {
            if (curentTries <= numberTries)
            {
                owner.SetDestination(randomDestination);
                curentTries++;
            }
            else
                owner.ChangeState(Patrol.Instance);
        }
    }

    public override void Exit(Enemy owner)
    {
        
    }
}
