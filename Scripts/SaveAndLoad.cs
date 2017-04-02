using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

[Serializable]public class SaveAndLoad {
    //Heartlight key
    static string EMPTYSPACE = "Default";
    static string PLAYER = "Tile_41";
    static string GRASS = "Grass";
    static string ROCK = "Tile_10";
    static string METALWALL = "Tile_20";
    static string HEART = "Tile_12";
    static string DOOR = "Tile_31";
    static string GRENADE = "Tile_02";
    static string BRICKWALL = "Tile_16";
    static string TUNNELLEFT = "Tile_19";
    static string TUNNELRIGHT = "Tile_34";
    static string PLASMA = "Tile_44";
    static string BALLOON = "Tile_04";

    public string[] lString;

    [SerializeField]
    public Main.GameType currentGameType;
    [SerializeField]
    public List<TextAsset> levels = new List<TextAsset>();
    public List<GridData> gridDataList = new List<GridData>();
    public List<string> hlLevelData = new List<string>();
    [SerializeField]public List<string> tileString = new List<string>();
    Main main;
    public void ConvertLevelData() {
        gridDataList.Clear();
        hlLevelData.Clear();
        levels.Clear();
        string appPath = " ";
        if(currentGameType == Main.GameType.heartlight) {
            appPath = "heartlight";
            char[] seperator = { '{', '}' };
            foreach(TextAsset text in Resources.LoadAll(appPath, typeof(TextAsset)).Cast<TextAsset>()) {
                levels.Add(text);
            }
            string levelString = levels[0].text;
            lString = levelString.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            foreach(string lv in lString) {
                lString[i] = Regex.Replace(lString[i], @"\n", "");
                if(lString != null) {
                    hlLevelData.Add(lString[i]);
                }
                i++;
            }

            ConvertHLData();

        }
        else if(currentGameType == Main.GameType.hypercycles) {
            appPath = "hypercycles";

            int i = 0;
            foreach(TextAsset text in Resources.LoadAll(appPath, typeof(TextAsset)).Cast<TextAsset>()) {
                levels.Add(text);
                i++;
            }

            ConvertHCData();
        }
        else {
            Debug.LogError("Wrong game type!");
            return;
        }
    }

    void SaveHLData() {
        int i = 1;
        foreach(GridData gd in gridDataList) {
            Debug.Log("Saving: Level" + i + "...");
            var appPath = Path.GetFullPath(".");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(appPath + "/Levels/" + "Level" + i + ".lvl", FileMode.Create);

            bf.Serialize(stream, gd);
            stream.Close();

            i++;
        }
    }

    void ConvertHLData() {
        
        int sizeX = 20;
        int sizeY = 12;
        int levelName = 0;
       // Debug.Log(tileString[0]);
        foreach(string hl in hlLevelData) {
            List <GridInfo> _gridInfo = new List<GridInfo>();
            char[] lv = hl.ToCharArray();
            
            string t = "";
            for(int y = 0, i = 0; y > -sizeY; y--) {
                for(int x = 0; x < sizeX; x++, i++) {
                    //Debug.Log(lv.Length + " " + i);
                    if(i < lv.Length) {
                        t = HL_SetCurrentTile(lv[i]);
                        if(lv[i] == '@' || lv[i] == '&' || lv[i] == '$')
                            _gridInfo.Add(new GridInfo(x, y, tileString[tileString.IndexOf(t)], Main.TileType.hl_physics));
                        else if(lv[i] == '.')
                            _gridInfo.Add(new GridInfo(x, y, tileString[tileString.IndexOf(t)], Main.TileType.hl_grass));
                        else if(lv[i] == '*')
                            _gridInfo.Add(new GridInfo(x, y, tileString[tileString.IndexOf(t)], Main.TileType.player));
                        else
                            _gridInfo.Add(new GridInfo(x, y, tileString[tileString.IndexOf(t)], Main.TileType.normal));
                    }
                    else {
                        t = "Default";
                        _gridInfo.Add(new GridInfo(x, y, tileString[tileString.IndexOf(t)], Main.TileType.normal));
                    }
                   
                }
            }

            gridDataList.Add(new GridData("Level" + levelName, sizeX, sizeY, _gridInfo, "Default", Main.GameType.heartlight));
            SaveHLData();
            levelName++;
        }

        

    }

    void ConvertHCData() {
        
        foreach(TextAsset levelData in levels) {
            char[] char_level;
            string string_level;

            string_level = levelData.text;
            string_level = string_level.Replace("\r", "").Replace("\n", "");
            char_level = string_level.ToCharArray();
            

        }
    }

    string HL_SetCurrentTile(char t) {
        string tile;

        if(currentGameType == Main.GameType.heartlight) {
            if(t == '*') tile = PLAYER;
            else if(t == '.') tile = GRASS;
            else if(t == '@') tile = ROCK;
            else if(t == '%') tile = METALWALL;
            else if(t == '$') tile = HEART;
            else if(t == '!') tile = DOOR;
            else if(t == '&') tile = GRENADE;
            else if(t == '#') tile = BRICKWALL;
            else if(t == '<') tile = TUNNELLEFT;
            else if(t == '>') tile = TUNNELRIGHT;
            else if(t == '=') tile = PLASMA;
            else if(t == '0') tile = BALLOON;
            else tile = EMPTYSPACE;
        }
        else {
            Debug.LogError("Error! HL_SetCurrentTile using wrong game type");
            return "Default";
        }
        return tile;
    }

    int HC_SetCurrentTile(char t) {
        int tile = 0;
        if(currentGameType == Main.GameType.hypercycles) {
            if(t == 'A') tile = 56;
            else if(t == 'B') tile = 16;
            else if(t == 'C') tile = 17;
            else if(t == 'D') tile = 20;
            else if(t == 'E') tile = 19;
            else if(t == 'F') tile = 0;
            else if(t == 'G') tile = 0;
            else if(t == 'H') tile = 0;
            else if(t == 'I') tile = 0;
            else if(t == 'J') tile = 0;
            else if(t == 'K') tile = 0;
            else if(t == 'L') tile = 0;
            else if(t == 'M') tile = 0;
            else if(t == 'N') tile = 0;
            else if(t == 'O') tile = 0;
            else if(t == 'P') tile = 26;
            else if(t == 'Q') tile = 22;
            else if(t == 'R') tile = 0;
            else if(t == 'S') tile = 0;
            else if(t == 'T') tile = 0;
            else if(t == 'U') tile = 0;
            else if(t == 'V') tile = 0;
            else if(t == 'W') tile = 0;
            else if(t == 'X') tile = 0;
            else if(t == 'Y') tile = 18;
            else if(t == 'Z') tile = 28;
         
        }
        return tile;
    }//sets tile based on alphabet letters

    public void SaveOldLevelData(int levelNum, GridData _gridData) {
        var path = Path.GetFullPath(".");
        FileStream stream;
        BinaryFormatter formatter = new BinaryFormatter();

        if(currentGameType == Main.GameType.hypercycles)
            stream = new FileStream(path + "/HClevels/" + levelNum.ToString() + ".lvl", FileMode.Create);
        else  if(currentGameType == Main.GameType.heartlight)
            stream = new FileStream(path + "/HLlevels/" + "Level" + levelNum.ToString() + ".lvl", FileMode.Create);
        else {
            Debug.LogError("Gametype not set!");
            return;
        }

        formatter.Serialize(stream, _gridData);
        stream.Close();

    }
}
