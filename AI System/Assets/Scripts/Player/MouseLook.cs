using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 40f;
    float mouseY = 0;
   
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetKey(KeyCode.Mouse1))
        {
            GetInputs();
        }

    }
    void GetInputs()
    {
        mouseY += Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        Mathf.Clamp(mouseY, 0, 90);
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, mouseY, transform.rotation.eulerAngles.z);
        
    }
}
