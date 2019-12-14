using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour
{
    public static GameObject GameobjectInstance = null;
    public static Player ClassInstance = null;
    private ThirdPersonUserControl movement;
    private Animator animator;

    public bool IsMoving()
    {

        return movement.isMoving;
    }

    void Awake()
    {
        if (GameobjectInstance == null)
            GameobjectInstance = this.gameObject;
        if (ClassInstance == null)
            ClassInstance = this;
        animator = GetComponent<Animator>();
        movement = GetComponent<ThirdPersonUserControl>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
