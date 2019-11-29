using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject Instance = null;



    void Awake()
    {
        if (Instance == null)
            Instance = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
