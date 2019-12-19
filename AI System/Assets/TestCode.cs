using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestCode : MonoBehaviour
{
    Light l;
    BoxCollider meshRenderer;
    
    public UnityEvent eveniment;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
        meshRenderer = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relative;
        relative = transform.InverseTransformDirection(transform.forward);
        Debug.Log(relative + " " + transform.forward + " " + Vector3.forward);
    }
}
