using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject aiController;
    public GameObject bot;
    public InputCommandObject inputCommandObject;
    public GameObject player;

    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        player = GameController.Player;
        foreach (Transform transform in transform)
        {
            GameObject curent = transform.gameObject;
            if (curent.tag == "AI")
                aiController = curent;
            else if (curent.tag == "Bot")
                bot = curent;

        }
    }
    void Awake()
    {
        player = GameController.Player;
        foreach(Transform transform in transform)
        {
            GameObject curent = transform.gameObject;
            if (curent.tag == "AI")
                aiController = curent;
            else if (curent.tag == "Bot")
                bot = curent;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
