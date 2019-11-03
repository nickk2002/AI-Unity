using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] public InputCommandObject InputCommandObject;
    private MeshRenderer meshRenderer;
    private Light light;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        light = transform.GetChild(0).GetComponent<Light>();
    }
    private void Update()
    {   
        light.color = InputCommandObject.DesiredColor;
    }
}
