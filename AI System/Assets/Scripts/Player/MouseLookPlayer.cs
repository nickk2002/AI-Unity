using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1.5f;
    GameObject player;
    void Start()
    {
        player = Player.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }
}
