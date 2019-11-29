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
    public bool alarm;
    public Vector3 lastSeendPlayer;
    public UnityEvent alarmEvent;

    public float alarmSpeed = 4f;
    public float shootingDistance = 5f;
    public float shootingDelay = 1f;
    public float searchRadius = 10f;
    public float patrolWaitTime = 1.5f;
    public int numberTries = 2;

    public AlarmState curentAlarmState;
    [SerializeField] private int easyAlerted;
    [SerializeField] private int mediumAlerted;
    public int numberAlerted;

}
