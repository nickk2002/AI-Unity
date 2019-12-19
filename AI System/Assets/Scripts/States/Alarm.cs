using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Alarm : State<Enemy>
{
    private float radius;
    private float curentTries,numberTries;
    private float shootingDistance;

    public Alarm()
    {

    }
    public override void Enter(Enemy owner)
    {
        Debug.Log("Entered Alarm!");
        owner.NavMeshAgent.speed = owner.aiState.alarmSpeed;

        radius = owner.aiState.alarmSearchRadius;
        shootingDistance = owner.aiState.shootingDistance;
        numberTries = owner.aiState.alarmSearchTries;

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
            lowerBound = radius / 2;
            upperBound = radius;
        }
        else
        {
            lowerBound = -radius;
            upperBound = 0;
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
            owner.ChangeState(owner.GetParticularState(typeof(Shoot)));
        if (owner.NavMeshAgent.remainingDistance < 1)
        {
            Debug.Log(curentTries);
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
                owner.aiState.alarmTriggered = false;
                owner.ChangeState(owner.GetParticularState(typeof(Patrol)));
            }
        }
    }

    public override void Exit(Enemy owner)
    {
        
    }
}
