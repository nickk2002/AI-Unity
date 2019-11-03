using UnityEngine;
using UnityEditor;

public class EnemyAsset
{
    [MenuItem("Assets/Create/InputCommandObject")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<InputCommandObject>();
    }
}