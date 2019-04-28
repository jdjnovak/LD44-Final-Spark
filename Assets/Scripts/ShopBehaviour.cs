using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBehaviour : MonoBehaviour
{
    public static bool[] clickable;

    private GameObject player;
    private int currentPlayerHealth;
    private int[] currentPlayerAbilityLevels; //TODO: Add this to player
    private Sprite[] shop_sprites;

    // Prices
    private int HealthUpCost;
    private int SpeedUpCost;
    private int ResistanceUpCost;
    private int EPSDownCost;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started shop!");
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerCurrentHealth();
    }

    void GetPlayerCurrentHealth() {
        currentPlayerHealth = player.GetComponent<Player>().GetCurrentHealth();
    }

    void GetPlayerCurrentAbilities() {
        currentPlayerAbilityLevels[0] = player.GetComponent<Player>().GetPlayerAbilities()[0];
        currentPlayerAbilityLevels[1] = player.GetComponent<Player>().GetPlayerAbilities()[1];
        currentPlayerAbilityLevels[2] = player.GetComponent<Player>().GetPlayerAbilities()[2];
        currentPlayerAbilityLevels[3] = player.GetComponent<Player>().GetPlayerAbilities()[3];
    }

    void SetCosts() {
        if (currentPlayerAbilityLevels[0] != 0) {
            HealthUpCost = 2 ^ (currentPlayerAbilityLevels[0] - 1) * 10;
        } else if (currentPlayerAbilityLevels[0] == 8) {
            HealthUpCost = -1;
        } else {
            HealthUpCost = 10;
        }

        if (currentPlayerAbilityLevels[1] != 0) {
            SpeedUpCost = currentPlayerAbilityLevels[1] * 5;
        } else if (currentPlayerAbilityLevels[1] == 10) {
            SpeedUpCost = -1;
        } else {
            SpeedUpCost = 5;
        }

        if (currentPlayerAbilityLevels[2] != 0) {
            ResistanceUpCost = 2 ^ (currentPlayerAbilityLevels[2] - 1) * 10;
        } else if (currentPlayerAbilityLevels[2] == 8) {
            ResistanceUpCost = -1;
        } else {
            ResistanceUpCost = 10;
        }

        if (currentPlayerAbilityLevels[3] != 0) {
            EPSDownCost = currentPlayerAbilityLevels[3] * 50;
        } else {
            EPSDownCost = 50;
        }
    }

    void SetIcons() {
        if (currentPlayerHealth > HealthUpCost) {
            GameObject.Find("MaxHealthUpButton").GetComponent<Image>().sprite = shop_sprites[0];
            clickable[0] = true;
        }
        
        if (currentPlayerHealth > SpeedUpCost) {
            GameObject.Find("MaxSpeedUpButton").GetComponent<Image>().sprite = shop_sprites[3];
            clickable[1] = true;
        }

        if (currentPlayerHealth > ResistanceUpCost) {
            GameObject.Find("ResistanceUpButton").GetComponent<Image>().sprite = shop_sprites[6];
            clickable[2] = true;
        }
        
        if (currentPlayerHealth > EPSDownCost) {
            GameObject.Find("EPSDownButton").GetComponent<Image>().sprite = shop_sprites[9];
            clickable[3] = true;
        }
    }

    void SetCostText() {
        Text text = GameObject.Find("HealthCostText").GetComponent<Text>();
        text.text = HealthUpCost.ToString() + " Energy";
        text = GameObject.Find("SpeedCostText").GetComponent<Text>();
        text.text = SpeedUpCost.ToString() + " Energy";
        text = GameObject.Find("ResistanceCostText").GetComponent<Text>();
        text.text = ResistanceUpCost.ToString() + " Energy";
        text = GameObject.Find("EPSCostText").GetComponent<Text>();
        text.text = EPSDownCost.ToString() + " Energy";
    }

    void SetEnergyText() {
        GameObject.FindGameObjectWithTag("UI_Shop_Energy_Text").GetComponent<Text>().text = currentPlayerHealth.ToString();
    }

    void Init() {
        player = GameObject.FindGameObjectWithTag("Player");
        shop_sprites = Resources.LoadAll<Sprite>("button_sprites");
        currentPlayerAbilityLevels = new int[4] { 0,0,0,0 };
        clickable = new bool[4] { false,false,false,false };
        GetPlayerCurrentAbilities();
        GetPlayerCurrentHealth();
        SetEnergyText();
        SetCosts();
        SetCostText();
        SetIcons();
    }

    private void OnEnable() {
        Init();
    }

    public void DisableCanvas() {
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<ShopBehaviour>().enabled = false;
        Time.timeScale = 1;
    }
}
