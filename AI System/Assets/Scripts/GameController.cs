using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    public static GameObject Player;
    public void CallCoroutine(IEnumerator enumerator)
    {
        Debug.Log("Start time coroutine");
        StartCoroutine(enumerator);
        Debug.Log("End time");
    }
    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        if (Instance == null)
            Instance = this;
    }
    private void Update()
    {
        ///StartCoroutine(F());
    }

}
