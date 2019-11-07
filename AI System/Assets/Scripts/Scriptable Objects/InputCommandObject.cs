using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputCommandManager",menuName ="AI/InputCommandObject")]
public class InputCommandObject : ScriptableObject
{
    public Vector3 destination;
    public Color desiredColor;
}
