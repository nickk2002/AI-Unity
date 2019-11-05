using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] public InputCommandObject inputCommandObject;
    EnemyController enemyController;
    private MeshRenderer meshRenderer;
    private Light light;

    private void Start()
    {
        GameObject parinte = transform.parent.gameObject;
        enemyController = parinte.GetComponent<EnemyController>();
        inputCommandObject = enemyController.inputCommandObject;
        meshRenderer = GetComponent<MeshRenderer>();
        light = transform.GetChild(0).GetComponent<Light>();
    }
    private void Update()
    {   
        light.color = inputCommandObject.desiredColor;
    }
}
