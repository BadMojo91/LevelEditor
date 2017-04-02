using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]public class GridData {
    public string name;
    public int[] size = new int[2];
    public List<GridInfo> gridInfo = new List<GridInfo>();
    public string floorName;
    public Main.GameType gameType;
    

    public GridData(string _name, int _x, int _y, List<GridInfo> _gridInfo, string _floor, Main.GameType _gameType) {
        size[0] = _x;
        size[1] = _y;
        gridInfo = _gridInfo;
        floorName = _floor;
        gameType = _gameType;
        name = _name;
    }

    public GridData(string _name, int _x, int _y, List<GridInfo> _gridInfo, string _floor) {
        size[0] = _x;
        size[1] = _y;
        gridInfo = _gridInfo;
        floorName = _floor;
        gameType = Main.GameType.other;
        name = _name;
    }
}
