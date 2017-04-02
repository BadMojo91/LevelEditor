using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]public class LevelSelector : MonoBehaviour {
    GameMain main;
    public void Awake() {
        main = GameObject.Find("GameGrid").GetComponent<GameMain>();
    }
    public void SelectLevel() {
        main.selectedLevel = GetComponentInChildren<Text>().text;
    }
}
