using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour
{
    private int currentPlayerHealth;
    private int[] currentPlayerAbilityLevels; //TODO: Add this to player

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started shop!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetPlayerCurrentHealth() {
        currentPlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetCurrentHealth();
    }

    void SetEnergyText() {
        GameObject.FindGameObjectWithTag("UI_Shop_Energy_Text").GetComponent<Text>().text = currentPlayerHealth.ToString();
    }

    private void OnEnable() {
        GetPlayerCurrentHealth();
        SetEnergyText();
    }

    public void DisableCanvas() {
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<ShopBehaviour>().enabled = false;
        Time.timeScale = 1;
    }
}
