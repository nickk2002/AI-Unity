using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum AlarmState
{
    AlarmEasy,
    AlarmMedium,
    AlarmHard,
}

[CreateAssetMenu(fileName ="AIStats",menuName ="AI/AIStats",order = 2)]
public class AIState : ScriptableObject
{
   


    /// Patrol State
    [Header("Patrol State")]
    public float patrolWaitTime = 1.5f;

    /// Shooting State
    [Header("Shooting State")]
    public float shootingDistance = 5f;
    public float shootingDelay = 1f;
    public float shootingUpdatePosDelay = 2f;

    /// Alert State
    [Header("Alert State")]
    public float alarmSpeed = 4f;
    public float alarmSearchRadius = 10f; 
    public int alarmSearchTries = 2;

    [Header("Related to alarm")]
    public bool alarmTriggered;
    public Vector3 lastSeendPlayer;
    public UnityEvent alarmEvent;
    public int maxNumberAlerted;
    public int numberAlerted;

}
