using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    // Start is called before the first frame update
    public AIState aiState;
    private AudioSource audioSource;
    void PlaySound()
    {
        if (audioSource.loop == false)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aiState.alarmEvent.AddListener(PlaySound);
    }

    // Update is called once per frame
    void Update()
    {   

    }
}
