using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { 

    // Player Variables
    public float maxVelocity;
    public float currentVelocity;
    public float moveBuffer; // For controller use, care for accidental hits of the joystick
    public int maxHealth;
    public int currentHealth;
    public float frameLength;
    public PlayerStates.States currentState;

    // Input Keys
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    // GameObject Components
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    // Other Objects
    private Sprite[] player_sprites;
    [SerializeField]
    private int currentSpriteFrame;
    [SerializeField]
    private int currentSpriteAnimation; // Based on player state
    private float lastFrameUpdate;

    // Start is called before the first frame update
    void Start() { 
        Init();
    }

    // Update is called once per frame
    void Update() {
        MovementUpdate();
        StateUpdate();
        SpriteUpdate();
    }

    void Init() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        player_sprites = Resources.LoadAll<Sprite>("spark");
        currentSpriteAnimation = 0;
        currentSpriteFrame = 0;
        maxHealth = currentHealth = 100;
        frameLength = 0.1f;
        lastFrameUpdate = Time.time;
    }

    void MovementUpdate() {
        if (Utilities.GetHypo(rb.velocity) <= maxVelocity) {
            rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * maxVelocity, 0.8f),
                                      Mathf.Lerp(0, Input.GetAxis("Vertical") * maxVelocity, 0.8f));
        }
    }

    void StateUpdate() {
        float healthPerc = (float)currentHealth / (float)maxHealth;
        if (healthPerc >= 0.81f) {
            currentState = PlayerStates.States.FullCharge;
        } else if (healthPerc >= 0.61f) {
            currentState = PlayerStates.States.HighCharge;
        } else if (healthPerc >= 0.41f) {
            currentState = PlayerStates.States.HalfCharge;
        } else if (healthPerc >= 0.21f) {
            currentState = PlayerStates.States.LowCharge;
        } else if (healthPerc >= 0.01f) {
            currentState = PlayerStates.States.NoCharge;
        } else if (healthPerc <= 0f) {
            currentState = PlayerStates.States.Dead;
        }
    }

    void SpriteAnimationUpdate() {
        currentSpriteAnimation = (int)currentState;
    }

    void SpriteFrameUpdate() {
        if (Time.time - lastFrameUpdate >= frameLength) {
            if (currentSpriteFrame == 3) {
                currentSpriteFrame = 0;
            } else {
                currentSpriteFrame++;
            }
            lastFrameUpdate = Time.time;
        }
        sr.sprite = player_sprites[currentSpriteFrame + (4 * currentSpriteAnimation)];
    }

    void SpriteUpdate() {
        SpriteAnimationUpdate();
        SpriteFrameUpdate();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
    }

    public bool IsDead() {
        return currentHealth <= 0;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }
}
