using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

[Serializable]public class GameGrid : MonoBehaviour {

    public List<GameObject> currentGrid;
    public Main.GameType currentGameType;
    public GameObject floorPlane;
    GridData currentGridData;
    List<GridInfo> currentGridInfo = new List<GridInfo>();
    public Sprite[] tileSprite;
    public List<String> tileSpriteList = new List<String>();

    public int gridX, gridZ;

    public List<Vector3> gridPos = new List<Vector3>();

    void Start() {
        LoadTileSprites();
        LoadLevelData("level1");
        CreateGameGrid();
    }

    public void LoadTileSprites() {      
        tileSprite = Resources.LoadAll<Sprite>("Sprites");
        foreach(Sprite s in tileSprite) {
            tileSpriteList.Add(s.name);
        }
    }

    public void UI_LoadLevel() {
        LoadLevelData(GetComponent<GameMain>().selectedLevel);
    }

    public void LoadLevelData(string path) {
        var appPath = Path.GetFullPath(".");
        if(File.Exists(appPath + "/Levels/" + path + ".lvl")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(appPath + "/Levels/" + path + ".lvl", FileMode.Open);

            GridData data = bf.Deserialize(stream) as GridData;

            stream.Close();

            currentGridData = data; 
            currentGridInfo.Clear();
            foreach(GridInfo gd in data.gridInfo) {
                //Debug.Log(gd.gridPos[0] + " " + gd.gridPos[1] + " " + gd.tileName);
                currentGridInfo.Add(gd);
            }
            gridX = data.size[0];
            gridZ = data.size[1];

            CreateGameGrid();
        }

    }

    void CreateGameGrid() {
        foreach(GameObject g in currentGrid) {
            Destroy(g);
        }
        currentGrid.Clear();
        gridPos.Clear();
        foreach(GridInfo gi in currentGridInfo) {
            if(gi.type != Main.TileType.hc_floor) {
                GameObject newTile = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newTile.transform.parent = this.transform;
                newTile.AddComponent<Rigidbody>();
                newTile.GetComponent<Rigidbody>().useGravity = false;
                newTile.GetComponent<BoxCollider>().isTrigger = true;
                if(currentGridData.gameType == Main.GameType.hypercycles)
                    newTile.transform.position = new Vector3(gi.gridPos[0], 0, gi.gridPos[1]);
                else if(currentGridData.gameType == Main.GameType.heartlight) {
                    newTile.transform.position = new Vector3(gi.gridPos[0], gi.gridPos[1], 0);

                    if(gi.type == Main.TileType.hl_physics) {
                        newTile.AddComponent<HL_Physics>();
                        newTile.GetComponent<HL_Physics>().speed = 5;
                    }
                    else if(gi.type == Main.TileType.hl_grass)
                        newTile.AddComponent<HL_Grass>();
                    else if(gi.type == Main.TileType.player)
                        newTile.AddComponent<Player>();
                }
                else
                    newTile.transform.position = new Vector3(gi.gridPos[0], gi.gridPos[1], 0);

            gridPos.Add(newTile.transform.position);
            newTile.GetComponent<MeshRenderer>().material.mainTexture = tileSprite[tileSpriteList.IndexOf(gi.tileName)].texture;
            currentGrid.Add(newTile);
            }
            else {
                gridPos.Add(new Vector3(gi.gridPos[0], 0, gi.gridPos[1]));
            }

            
        }

        if(currentGridData.gameType == Main.GameType.hypercycles) {
            floorPlane.transform.localScale = new Vector3(((float)gridX / 10f), 1, ((float)gridZ / 10f));
            floorPlane.transform.position = new Vector3((gridX / 2) - 0.5f, -0.5f, (-gridZ / 2) + 0.5f);
            floorPlane.GetComponent<MeshRenderer>().material.mainTexture = tileSprite[tileSpriteList.IndexOf(currentGridData.floorName)].texture;
            floorPlane.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(gridX, gridZ);
        }
    }      
}



