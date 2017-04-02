using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;

[Serializable]public class GameMain : MonoBehaviour {

    [SerializeField]
    public string selectedLevel; 
    public GameObject defaultButton;
    public GameObject levelListPanel;
    public List<Text> levelList;
    public List<GridData> levelData = new List<GridData>();
    public List<string> levelNameList = new List<string>();
    void Start() {
        LoadAllLevels();
    }
   
    void LoadAllLevels() {

        foreach(string file in Directory.GetFiles(Path.GetFullPath(".") + "/Levels/")) {
            if(Path.GetExtension(file) == ".lvl") {
                Debug.Log(file);
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(file, FileMode.Open);

                GridData gd = formatter.Deserialize(stream) as GridData;

                stream.Close();
                levelData.Add(gd);

            }
        }

        foreach(GridData gd in levelData) {
            GameObject addLevel = Instantiate(defaultButton, levelListPanel.transform);
            addLevel.GetComponentInChildren<Text>().text = gd.name;
        }

        
    }
}
