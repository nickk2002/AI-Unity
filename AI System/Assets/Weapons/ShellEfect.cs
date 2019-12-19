using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShellEfect : MonoBehaviour
{


    [Header("Force")]

    [Header("ForceX")]
    [SerializeField] private float minForceX;
    [SerializeField] private float maxForceX;
    [Header("ForceY")]
    [SerializeField] private float minForceY;
    [SerializeField] private float maxForceY;
    [Header("ForceZ")]
    [SerializeField] private float minForceZ;
    [SerializeField] private float maxForceZ;
    [SerializeField] private float fortaEject; // forta cu care iese


    [Header("Rotation")]
    [SerializeField] private float minRotation;
    [SerializeField] private float maxRotation;

    Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 torqueRotation = new Vector3(
            Random.Range(minRotation, maxRotation),
            Random.Range(minRotation, maxRotation),
            Random.Range(minRotation, maxRotation)
        );
        rb.AddRelativeTorque(torqueRotation * Time.deltaTime);

        Vector3 directionForce = new Vector3(
            Random.Range(minForceX, maxForceX),
            Random.Range(minForceY, maxForceY),  //Y Axis
            Random.Range(minForceZ, maxForceZ)
        );
        rb.AddRelativeForce(directionForce, ForceMode.Force);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.right, fortaEject * Time.deltaTime);
        transform.Rotate(Vector3.down, fortaEject * Time.deltaTime);
    }
}
