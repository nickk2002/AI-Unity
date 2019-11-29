using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI textMeshPro;
    [SerializeField] private PlayerState playerState;
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
    }
    public void UpdateText()
    {
        textMeshPro.text = "Health: " + playerState.curentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();   
    }
}
