﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameController.Player.transform;
        offset = player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = transform.position = player.position - offset;
        Vector3 actualPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

    }
}