using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class Grid : MonoBehaviour {
    Main main;
    public int gridX;
    public int gridY;
    public Sprite defaultSprite;
    public GameObject tileButton;
    public RectTransform sidePanel;
    public List<GameObject> gridList = new List<GameObject>();
    public List<Sprite> tile = new List<Sprite>();
    public List<string> tileName = new List<string>();
    public GridData currentGridData;
    public List<GridInfo> currentGridInfo = new List<GridInfo>();
    public SaveAndLoad _saveAndLoad = new SaveAndLoad();

    List<string> levelString = new List<string>();
    string[] levelData;

    public static void SaveGrid(GridData _gridData, string path) {
        var appPath = Path.GetFullPath(".");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(appPath + "/Levels/" + path + ".lvl", FileMode.Create);

        bf.Serialize(stream, _gridData);
        stream.Close();
    }

    public void LoadGrid(string path) {
        var appPath = Path.GetFullPath(".");
        if(File.Exists(appPath + "/Levels/" + path + ".lvl")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(appPath + "/Levels/" + path + ".lvl", FileMode.Open);

            GridData data = bf.Deserialize(stream) as GridData;

            stream.Close();

            currentGridInfo.Clear();

            foreach(GridInfo gd in data.gridInfo) {
                //Debug.Log(gd.gridPos[0] + " " + gd.gridPos[1] + " " + gd.tileName);
                currentGridInfo.Add(gd);
            }

            gridX = data.size[0];
            gridY = data.size[1];
            Camera.main.transform.position = new Vector3(gridX / 2, -gridY / 2, -10);
            Camera.main.orthographicSize = 11;
            CreateGrid();
            main.currentLevelName = path;
        }
        else {
            Debug.LogError("File Not Found:" + appPath);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(appPath + "/Levels/" + path + ".lvl", FileMode.Create);

            bf.Serialize(stream, "");
            stream.Close();
            //Debug.Log(Resources.Load("/Levels/" + path + ".lvl") as );
            return;
        }


    }
    void Update() {
        if(main.enableConversion)
            if(Input.GetKeyDown("f1")) {
                Debug.Log("Loading...");
                _saveAndLoad.ConvertLevelData();
            }
    }

    void Start() {
        //WriteToFile("/test.txt");
        //ReadFromFile("/test.txt");
        main = GameObject.Find("Main").GetComponent<Main>();
        Init();

        LoadGrid("Template");
        //Convert Level Data
        _saveAndLoad.tileString = tileName;
        //_saveAndLoad.ConvertLevelData();
    }



    public void UI_LoadPath(string path) {
        Debug.Log(path + ": Loading...");
        //ReadFromFile(path);
        LoadGrid(path);
    }

    public void UI_SavePath(string path) {
        Debug.Log(path + ": Saving...");
        //WriteToFile("/" + path + ".txt");

        List<GridInfo> gd = new List<GridInfo>();
        foreach(GameObject t in gridList) {
            if(t.GetComponent<GridEditor>().hasHLPhysics)
                gd.Add(new GridInfo(t.transform.position.x, t.transform.position.y, t.GetComponent<SpriteRenderer>().sprite.name, Main.TileType.hl_physics));
            else if(t.GetComponent<GridEditor>().isFloor)
                gd.Add(new GridInfo(t.transform.position.x, t.transform.position.y, t.GetComponent<SpriteRenderer>().sprite.name, Main.TileType.hc_floor));
            else if(t.GetComponent<GridEditor>().isGrass)
                gd.Add(new GridInfo(t.transform.position.x, t.transform.position.y, t.GetComponent<SpriteRenderer>().sprite.name, Main.TileType.hl_grass));
            else
                gd.Add(new GridInfo(t.transform.position.x, t.transform.position.y, t.GetComponent<SpriteRenderer>().sprite.name, Main.TileType.normal));
            // if(t.GetComponent<SpriteRenderer>().sprite.name != "Default")
            //Debug.Log(t.GetComponent<SpriteRenderer>().sprite.name);
        }
        
        currentGridData = new GridData(main.currentLevelName, gridX, gridY, gd, main.floorSprite.name, main.currentGameType);

        SaveGrid(currentGridData, path);
    }

    void WriteToFile(string path) {
        int indexSize = gridList.Count + 2;
        string[] levelData = new string[indexSize];
        string[] size = {gridX.ToString(), gridY.ToString()};
        for(int i = 0; i < levelData.Length; i++) {
            if(i < 2)
                levelData[i] = size[i];
            else
                levelData[i] = gridList[i - 2].GetComponent<SpriteRenderer>().sprite.name;
        }
        File.WriteAllLines(Application.dataPath + path, levelData);
    }

    void LoadData() {
        CreateGrid();
        for(int i = 0; i < gridList.Count; i++) {
            int spriteIndex = tileName.IndexOf(levelString[i]);
            if(spriteIndex < tile.Count)
                gridList[i].GetComponent<SpriteRenderer>().sprite = tile[spriteIndex];
            else
                Debug.Log("Error! index of gridList");
        }
    }

    void Init() {
        int i = 0;
        foreach(Sprite spr in Resources.LoadAll("Sprites", typeof(Sprite)).Cast<Sprite>().ToArray()) {

            tile.Add(spr);
            tileName.Add(spr.name);
        }

        foreach(Sprite spr in tile) {
            GameObject newTile = Instantiate(tileButton, sidePanel);
            newTile.GetComponent<Image>().sprite = spr;
            newTile.name = "Tile" + i;
            newTile.AddComponent<EventTrigger>();
            //newTile.AddComponent<Tile>();
            i++;
        }
    }

    public void CreateGrid() {
        ClearGrid();
        int i = 0;
        foreach(GridInfo gi in currentGridInfo) {
            GameObject newObj = new GameObject();
            newObj.transform.parent = this.transform;
            newObj.AddComponent<BoxCollider2D>();
            newObj.AddComponent<GridEditor>();
            newObj.AddComponent<SpriteRenderer>();
            newObj.GetComponent<GridEditor>().tileType = gi.type;
            newObj.GetComponent<SpriteRenderer>().sprite = tile[tileName.IndexOf(gi.tileName)];
            newObj.transform.position = new Vector3(currentGridInfo[i].gridPos[0], currentGridInfo[i].gridPos[1], 0);
            newObj.name = "Pos" + i;
            gridList.Add(newObj);
            i++;
        }
    }

    public void CreateNewGrid() {
        ClearGrid();
        currentGridInfo.Clear();
        if(gridList.Count >= gridX * gridY)
            return;
        int i = 0;
        for(int y = 0; y > -gridY; y--) {
            for(int x = 0; x < gridX; x++) {
                GameObject newObj = new GameObject();
                newObj.transform.parent = this.transform;
                newObj.AddComponent<BoxCollider2D>();
                newObj.AddComponent<GridEditor>();
                newObj.AddComponent<SpriteRenderer>();
                //Debug.Log(tileName.IndexOf(levelString[i]));
                newObj.GetComponent<SpriteRenderer>().sprite = defaultSprite;

                newObj.transform.position = new Vector3(x, y, 0);
                newObj.name = "Pos" + i;
                gridList.Add(newObj);
                currentGridInfo.Add(new GridInfo(x, y));
                i++;
            }
        }

        currentGridData = new GridData("temp", gridX, gridY, currentGridInfo, main.floorSprite.name, main.currentGameType);

    }

    public void ClearGrid() {
        foreach(GameObject obj in gridList)
            Destroy(obj);

        gridList.Clear();
    }

}
