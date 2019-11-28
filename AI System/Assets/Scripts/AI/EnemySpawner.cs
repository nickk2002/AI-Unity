using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject gameObjectWithChildren;
    
    private EnemyController enemyController;

    void Start()
    {

        
        if (gameObjectWithChildren && spawnPositions.Length == 0)
        {
            int i = 0;
            spawnPositions = new Transform[gameObjectWithChildren.transform.childCount];
            foreach (Transform transform in gameObjectWithChildren.transform)
            {
                spawnPositions[i++] = transform;
            }
        }
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            Transform transform = spawnPositions[i];
            EnemyState scriptableObject = ScriptableObject.CreateInstance("InputCommandObject") as EnemyState;
            scriptableObject.desiredColor = new Color(Random.value, Random.value, Random.value);
            AssetDatabase.CreateAsset(scriptableObject, "Assets/ScriptableObjects/Enemy" + i);
            GameObject enemy = Instantiate(prefab, transform.position, transform.rotation);
            
            enemyController = enemy.GetComponent<EnemyController>();
            enemyController.inputCommandObject = scriptableObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
