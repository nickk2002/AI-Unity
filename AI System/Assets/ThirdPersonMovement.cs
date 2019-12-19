using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    Animator animator; // este pus de ei in asa fel incat pe posx sa fie turn, si pe pozy sa fie forward
    float forwardAmount, turnAmount;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void GetInputs()
    {
        float lateral = Input.GetAxis("Horizontal"); //  lateral
        float fata = Input.GetAxis("Vertical");  // fata

        Vector3 move = camera.transform.right * lateral + camera.transform.forward * fata;

        Move(move);
        
    }
    void Move(Vector3 move)
    {
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;
    }
    void UpdateAnimator()
    {
        animator.SetFloat("Forward", forwardAmount);
        animator.SetFloat("Turn", turnAmount);
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInputs();
        UpdateAnimator();
    }
}
