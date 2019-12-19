using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 40f;

    float mouseY = 0,mouseX = 0;
   
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse1) && !Player.ClassInstance.IsMoving())
        {
            GetInputs();
        }
    }
    void GetInputs()
    {
        mouseY = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        mouseX = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
    
        Mathf.Clamp(mouseY, 0, 90);
        Mathf.Clamp(mouseY, 15, 15);
        Debug.Log("Mouse Y" + mouseY);
        float angleRotation = mouseY;

        if(mouseY != 0 && Mathf.Abs(mouseX) < 0.4f)
            transform.RotateAround(Player.GameobjectInstance.transform.position, Player.GameobjectInstance.transform.up, angleRotation);
        //else if(mouseX != 0 && Mathf.Abs(mouseY) < 0.4f)
            //transform.RotateAround(Player.GameobjectInstance.transform.position, Player.GameobjectInstance.transform.right, mouseX);
        //transform.LookAt(Player.GameobjectInstance.transform);
    }
}
