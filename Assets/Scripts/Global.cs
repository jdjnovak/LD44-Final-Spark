using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private int lossPerSecond;

    private GameObject player;
    private float lastTookDamage;
    private Text scoreText;
    private Text healthText;
    private Image healthImage;
    private Sprite[] health_sprites;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
        LPSUpdate();
        DamageUpdate();
        ScoreUIUpdate();
        HealthTextUpdate();
        HealthImageUpdate();
    }

    void Init() {
        score = 0;
        lossPerSecond = 1;
        lastTookDamage = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        health_sprites = Resources.LoadAll<Sprite>("health");
    }

    void LPSUpdate() {
        // This is the main source of difficulty
        // the higher this is, the faster your life
        // depletes as you play.
        // < 30 sec - 1
        // < 1 min - 2
        // < 2 min - 4
        // etc...
        if (score <= 30) {
            lossPerSecond = 1;
        } else if (score <= 60) {
            lossPerSecond = 2;
        } else if (score < 120) {
            lossPerSecond = 4;
        } // and more...
    }

    void ScoreUpdate() {
        score = Mathf.RoundToInt(Time.time);
    }

    void DamagePlayer() {
        lastTookDamage = Time.time;
        player.GetComponent<Player>().TakeDamage(lossPerSecond);
    }

    bool TimeToDamagePlayer() {
        return Time.time - lastTookDamage >= 1f;
    }

    void DamageUpdate() {
        if (TimeToDamagePlayer()) {
            DamagePlayer();
        }
    }

    void ScoreUIUpdate() {
        scoreText.text = score.ToString();
    }

    void HealthTextUpdate() {
        healthText.text = player.GetComponent<Player>().GetCurrentHealth().ToString() + "/" + player.GetComponent<Player>().GetMaxHealth().ToString();  
    }

    void HealthImageUpdate() {
        float healthPerc = (float)player.GetComponent<Player>().GetCurrentHealth() / (float)player.GetComponent<Player>().GetMaxHealth();
        if (healthPerc >= 0.91f) {
            healthImage.sprite = health_sprites[0];
        } else if (healthPerc >= 0.81f) {
            healthImage.sprite = health_sprites[1];
        } else if (healthPerc >= 0.71f) {
            healthImage.sprite = health_sprites[2];
        } else if (healthPerc >= 0.61f) {
            healthImage.sprite = health_sprites[3];
        } else if (healthPerc >= 0.51f) {
            healthImage.sprite = health_sprites[4];
        } else if (healthPerc >= 0.41f) {
            healthImage.sprite = health_sprites[5];
        } else if (healthPerc >= 0.31f) {
            healthImage.sprite = health_sprites[6];
        } else if (healthPerc >= 0.21f) {
            healthImage.sprite = health_sprites[7];
        } else if (healthPerc >= 0.11f) {
            healthImage.sprite = health_sprites[8];
        } else if (healthPerc >= 0.1f) {
            healthImage.sprite = health_sprites[9];
        }
    }
}
