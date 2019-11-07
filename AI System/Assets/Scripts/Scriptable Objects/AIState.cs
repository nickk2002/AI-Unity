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
    // Start is called before the first frame update
    public UnityEvent alarmEvent;
    public AlarmState curentAlarmState;
    [SerializeField] private int easyAlerted;
    [SerializeField] private int mediumAlerted;
    public int numberAlerted;


}
