using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetCurrentTile : MonoBehaviour {

    Main main;
    Image im;

    void Awake() {
        im = GetComponent<Image>();
        main = GameObject.Find("Main").GetComponent<Main>();
    }

    void Update() {
        if(this.name == "CurrentTile") 
            im.sprite = main.currentSprite;
        else if(this.name == "CurrentFloor") 
            im.sprite = main.floorSprite;
    }
}
