using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputCommandManager",menuName ="AI/InputCommandObject")]
public class EnemyState : ScriptableObject
{
    public Vector3 destination;
    public Color desiredColor;
    public Transform[] positions;
    public float distance;
    public float angle;
}
