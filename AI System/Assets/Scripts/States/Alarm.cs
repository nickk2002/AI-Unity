using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

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
        radius = owner.aiState.alarmSearchRadius;
        shootingDistance = owner.aiState.shootingDistance;
        curentTries = owner.aiState.alarmInvestigateTries;
        owner.SetDestination(owner.aiState.lastSeendPlayer);
        owner.aiState.alarmEvent.Invoke();
        curentTries = 0;
    }
    public Tuple<float, float> SearchBounds()
    {
        float number = UnityEngine.Random.Range(0, 1);
        float lowerBound, upperBound;
        if (number <= 0.7) /// se duce in fata cu un procentaj de 70%
        {
            lowerBound = 0;
            upperBound = radius;
        }
        else
        {
            lowerBound = -radius;
            upperBound = radius;
        }
        return Tuple.Create(lowerBound, upperBound);
    }
    Vector3 RandomSearchPosition()
    {
        Tuple<float, float> bounds = SearchBounds();
        float lowerBound = bounds.Item1;
        float upperBound = bounds.Item2;
        Vector3 randomPosition =
        new Vector3(UnityEngine.Random.Range(lowerBound, upperBound), UnityEngine.Random.Range(lowerBound, upperBound), UnityEngine.Random.Range(lowerBound, upperBound));
        Debug.Log(randomPosition);
        return randomPosition;
    }
    public override void Execute(Enemy owner)
    {
       
        if (owner.CanSeePlayer() && owner.DistanceToPlayer() <= shootingDistance)
            owner.ChangeState(Shoot.Instance);
        if (owner.NavMeshAgent.remainingDistance < 1)
        {
            if (curentTries <= numberTries)
            {
                Vector3 randomDestination = RandomSearchPosition() + owner.aiState.lastSeendPlayer;
                owner.SetDestination(randomDestination);
                curentTries++;
            }
            else
            {
                Debug.Log("go to patrol");
                owner.aiState.numberAlerted = 0;
                owner.ChangeState(Patrol.Instance);
            }
        }
    }

    public override void Exit(Enemy owner)
    {
        
    }
}
