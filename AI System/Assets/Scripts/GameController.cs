using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    public static GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        if (Instance == null)
            Instance = this;
    }


}
