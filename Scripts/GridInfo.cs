using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GridInfo {
    
    public float[] gridPos = new float[2];
    public string tileName;
    public Main.TileType type;    
    

    public GridInfo(float _x, float _y, string _tile, Main.TileType _type) {
        gridPos[0] = Mathf.RoundToInt(_x);
        gridPos[1] = Mathf.RoundToInt(_y);
        tileName = _tile;
        type = _type;
    }

    public GridInfo(float _x, float _y) {
        gridPos[0] = Mathf.RoundToInt(_x);
        gridPos[1] = Mathf.RoundToInt(_y);
        tileName = "Default";
        type = Main.TileType.normal;
    }
}
