using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 35f;
    [SerializeField] private float turnBackSpeed = 15f;
    private float mouseY = 0;

    Quaternion initialRotation;
    void Start()
    {
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update() {

        //Debug.Log(transform.localEulerAngles.y);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mouseY = 0;   
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (Camera.main)
                Camera.main.GetComponent<Camera>().enabled = false;
            Debug.Log("Moving");
            GetInput();
        }
        else
        {
            if (transform.eulerAngles.y < 180)
            {
                Quaternion rotation = Quaternion.Slerp(transform.localRotation, initialRotation, 0.5f);
                Debug.Log(rotation);
                transform.localRotation = rotation;
            }
            else
            {
                Debug.Log("invers");
                Quaternion rotation = Quaternion.Slerp(initialRotation, transform.localRotation, 0.5f);
                transform.localRotation = rotation;
            }

        }
        if (transform.localRotation == initialRotation && Camera.main)
        {
            Camera.main.GetComponent<Camera>().enabled = true;
        }

    }
    void GetInput()
    {
        mouseY += Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        Mathf.Clamp(mouseY, -360, 360);
        transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, mouseY, transform.rotation.eulerAngles.z);
    }
}
