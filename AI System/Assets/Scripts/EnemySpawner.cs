using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Prefab;
    [SerializeField] Transform[] Positions;
    [SerializeField] GameObject ChildGameObject;


    void UpdatePlayer(GameObject Enemy, InputCommandObject script)
    {
        foreach (Transform transform in Enemy.transform)
        {
            Debug.Log(transform.gameObject.tag);
            if (transform.gameObject.tag == "Player")
            {
                GameObject Player = transform.gameObject;
                Movement movement = Player.gameObject.GetComponent<Movement>();
                movement.InputCommandObject = script;
                Debug.Log("AM GASIT CE TREBUIA");
                ChangeColor changecolor = Player.gameObject.GetComponent<ChangeColor>();
                changecolor.InputCommandObject = script;
            }
        }
    }
    void UpdateAI(GameObject Enemy,InputCommandObject script)
    {
        foreach(Transform transform in Enemy.transform)
        {
            if(transform.gameObject.tag == "AI")
            {
                GameObject AI = transform.gameObject;
                FSM fsm = AI.gameObject.GetComponent<FSM>();
                fsm.inputCommandObject = script;
            }
        }
    }
    void Start()
    {
        if(ChildGameObject && Positions.Length == 0)
        {
            int i = 0;
            Positions = new Transform[ChildGameObject.transform.childCount];
            foreach (Transform transform in ChildGameObject.transform)
            {
                Positions[i++] = transform;
            }
        }
        for(int i = 0; i < Positions.Length; i++)
        {
            Transform transform = Positions[i];
            InputCommandObject script = ScriptableObject.CreateInstance("InputCommandObject") as InputCommandObject;
            script.DesiredColor = new Color(Random.value, Random.value, Random.value);
            AssetDatabase.CreateAsset(script, "Assets/SciptableObjects/Enemy" + i);
            GameObject Enemy = Instantiate(Prefab, transform.position, transform.rotation);
            UpdatePlayer(Enemy, script);
            UpdateAI(Enemy, script);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
