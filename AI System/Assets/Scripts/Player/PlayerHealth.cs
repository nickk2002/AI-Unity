using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float curentHealth;
    private float maxHealth;
    public PlayerState playerState;
    void Awake()
    {
        curentHealth = playerState.maxHealth;
        maxHealth = playerState.maxHealth;
        playerState.TakenDamageEvent.AddListener(TakeDamage);
    }

    private void TakeDamage(float damage)
    {
        curentHealth -= damage;
        playerState.curentHealth = curentHealth;
        if (curentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
