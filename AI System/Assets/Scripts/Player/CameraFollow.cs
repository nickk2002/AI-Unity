using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool offsetScene = true;
    public Vector3 offset = new Vector3(0f, 7.5f, 0f);

    private bool lastMoveState;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void UpdatePosition()
    {
        offset = target.position - transform.position;
    }
    void SetPosition()
    {
        transform.position = target.position - offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Player.ClassInstance.IsMoving())
        {
            if (lastMoveState == false)
            {
                UpdatePosition();
                Debug.Log("plsss");
            }
            SetPosition();
        }
        lastMoveState = Player.ClassInstance.IsMoving();
    }
}
