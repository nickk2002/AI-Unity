using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private InputCommandObject InputCommandObject;
    [SerializeField] private MeshRenderer meshRenderer;
    private Light light;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        light = transform.GetChild(0).GetComponent<Light>();
        Debug.Log(light);
    }
    private void Update()
    {   
        light.color = InputCommandObject.DesiredColor;
        //meshRenderer.material.color = InputCommandObject.DesiredColor;
    }
}
