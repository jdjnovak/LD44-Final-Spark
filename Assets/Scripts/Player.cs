using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour { 

    public enum States {
        FullCharge,
        HighCharge,
        HalfCharge,
        LowCharge,
        NoCharge,
        Dead = -1
    }

    public float maxVelocity;
    public float currentVelocity;
    public float moveBuffer; // For controller use

    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() { 
        Init();
    }

    // Update is called once per frame
    void Update() {
        MovementUpdate();
    }

    void Init() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void MovementUpdate() {
        //rb.AddForce(new Vector2(maxVelocity * Input.GetAxis("Horizontal"), maxVelocity * Input.GetAxis("Vertical")));
        if (Utilities.GetHypo(rb.velocity) <= maxVelocity) {
            rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * maxVelocity, 0.8f),
                                      Mathf.Lerp(0, Input.GetAxis("Vertical") * maxVelocity, 0.8f));
        }
    }
}
