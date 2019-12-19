using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public bool Move = false;
    [SerializeField] private Camera camera;
    public static GameObject GameobjectInstance = null;
    public static Player ClassInstance = null;
    private Animator animator;
    private ThirdPersonCharacter character;
    private bool crouch, jump;
    private float forward,horizontal;
   

    void GetInputs()
    {
        forward = Input.GetAxis("Vertical");
        Vector3 Move = camera.transform.forward * forward;
        if (forward != 0)
        {
            //horizontal = Input.GetAxis("Mouse X");

            //transform.Rotate(0, horizontal * 5, 0);
              
        }
        character.Move(Move, crouch, jump);
        jump = false;
    }

    public bool IsMoving()
    {
        if (forward != 0)
            return true;
        return false;
    }

    void Awake()
    {
        if (Move)
            GetComponent<ThirdPersonUserControl>().enabled = false;
        if (GameobjectInstance == null)
            GameobjectInstance = this.gameObject;
        if (ClassInstance == null)
            ClassInstance = this;
        animator = GetComponent<Animator>();
        character = GetComponent<ThirdPersonCharacter>();
    }
    // Update is called once per frame
    void Update()
    {
        crouch = Input.GetKeyDown(KeyCode.C);
        if (!jump)
            jump = CrossPlatformInputManager.GetButton("Jump");

    }
    private void FixedUpdate()
    {
        if(Move)
            GetInputs();
        
    }
}
