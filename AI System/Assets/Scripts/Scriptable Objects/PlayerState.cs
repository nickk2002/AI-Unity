using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Player/PlayerHealth", order = 3)]
public class PlayerState : ScriptableObject
{
    public int health;
    public int maxHealth;
}
