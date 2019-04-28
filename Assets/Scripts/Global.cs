﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global : MonoBehaviour
{
    private static GameObject player;

    [SerializeField]
    private int score;
    [SerializeField]
    private int energyPerSecond;

    private bool incrementedEPS;
    private float lastIncrementedEPS;
    private float lastTookDamage;
    private Text scoreText;
    private Text epsText;
    private Text healthText;
    private Image healthImage;
    private Sprite[] health_sprites;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Yes, yes, I should have put all the UI stuff into a UI manager class...
    void Update()
    {
        ScoreUpdate();
        EPSUpdate();
        DamageUpdate();
        ScoreUIUpdate();
        CheckIfTimeToIncrementEPS();
        EPSTextUpdate();
        HealthTextUpdate();
        HealthImageUpdate();
    }

    void Init() {
        score = 0;
        incrementedEPS = false;
        energyPerSecond = 1;
        lastTookDamage = lastIncrementedEPS = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        epsText = GameObject.Find("EPSText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthImage = GameObject.Find("HealthImage").GetComponent<Image>();
        health_sprites = Resources.LoadAll<Sprite>("health");
    }

    void EPSUpdate() {
        // TODO: Think of better solution to this
        //       i.e. how to make hard but still fun
        //            but still playable?
        if (score % 15 == 0 && score != 0 && !incrementedEPS && CheckIfTimeToIncrementEPS()) {
            energyPerSecond++;
            incrementedEPS = true;
            lastIncrementedEPS = Time.time;
        }
    }

    bool CheckIfTimeToIncrementEPS() {
        if (Time.time - lastIncrementedEPS >= 1f) {
            incrementedEPS = false;
            return true;
        }
        return false;
    }

    void ScoreUpdate() {
        score = Mathf.RoundToInt(Time.time);
    }

    void DamagePlayer() {
        lastTookDamage = Time.time;
        player.GetComponent<Player>().TakeDamage(energyPerSecond);
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

    void EPSTextUpdate() {
        epsText.text = "-" + energyPerSecond.ToString() + " energy/sec";
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

    public static GameObject GetPlayer() {
        return player;
    }
}
