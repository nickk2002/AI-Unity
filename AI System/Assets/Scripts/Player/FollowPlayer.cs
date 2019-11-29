using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.Instance.transform.position;
        player = Player.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 actualPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = actualPosition;
    }
}
