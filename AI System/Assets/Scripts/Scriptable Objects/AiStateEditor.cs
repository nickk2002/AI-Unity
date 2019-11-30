using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIState))]
[CanEditMultipleObjects]
public class AiStateEditor : Editor
{
    SerializedProperty alarm;

}
