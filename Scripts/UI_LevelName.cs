using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_LevelName : MonoBehaviour {
    Text levelName;
    GameMain main;
    void Awake() {
        main = GameObject.Find("GameGrid").GetComponent<GameMain>();
        levelName = GetComponent<Text>();
    }
    void Update () {
        levelName.text = main.selectedLevel;
	}
}
