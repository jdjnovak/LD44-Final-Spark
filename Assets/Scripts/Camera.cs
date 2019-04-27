using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        PositionUpdate();
    }

    void Init() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void PositionUpdate() {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -2.5f);
    }
}
