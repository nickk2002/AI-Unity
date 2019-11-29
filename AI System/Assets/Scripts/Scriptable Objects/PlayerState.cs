using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class FloatEvent : UnityEvent<float>
{

}

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Player/PlayerHealth", order = 3)]
public class PlayerState : ScriptableObject
{
    public float curentHealth;
    public float maxHealth;

    public FloatEvent TakenDamageEvent;

    void Start()
    {
        curentHealth = maxHealth;
    }
}
