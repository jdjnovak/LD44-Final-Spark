using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: Make buttons swap sprites on mouse enter
    private void OnMouseEnter() {
        switch (gameObject.name) {
            case "MaxHealthUpButton":
                break;
            case "MaxSpeedUpButton":
                break;
            case "ResistanceUpButton":
                break;
            case "EPSDownButton":
                break;
        }
    }

    private void OnMouseExit() {
    }
}
