using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]public class Main : MonoBehaviour {
    public Sprite currentSprite;
    [SerializeField]
    public Sprite highlightedSprite;
    [SerializeField]
    public Sprite floorSprite;
    [SerializeField]
    public bool setFloor;
    [SerializeField]
    public TileType currentTileType;
    public enum TileType {player, normal, hl_grass, hl_physics, hc_floor}
    public enum GameType {heartlight, hypercycles, other};
    public GameType currentGameType;
    public GameObject sideMenu;
    public GameObject settingsMenu;
    public GameObject menuButton;
    public GameObject loadMenu;
    public GameObject saveMenu;
    public Text loadText;
    public Text saveText;
    public Text xSize, ySize;
    public Text tileInfo, tileInfo2;
    public Text currentSpriteName;
    public bool enableConversion;
    public bool loadMenuOpen;
    public bool saveMenuOpen;
    public bool tileMenuOpen;
    public bool settingsMenuOpen;
    [SerializeField]public string currentLevelName;

    public Dropdown gameModeDropdown;
    public Dropdown tileTypeDropdown;
    [SerializeField]public Grid grid;

    void Awake() {
        currentSprite = grid.defaultSprite;
        floorSprite = grid.defaultSprite;
        tileMenuOpen = true;
        //sideMenu.SetActive(false);
        //settingsMenu.SetActive(false);
        //menuButton.SetActive(true);
        //loadMenu.SetActive(false);
        loadMenuOpen = false;
        //saveMenu.SetActive(false);
        saveMenuOpen = false;
        setFloor = false;
       
    }
    void Start() {
        
    }
    void Update() {
        UI_Update();


        //Input
        if(!loadMenuOpen && !saveMenuOpen && !settingsMenuOpen)
            if(Input.GetKeyDown("n"))
                UI_SetFloor();

    }

    void UI_DisableAllMenus() {
        loadMenuOpen = false;
        saveMenuOpen = false;
        settingsMenuOpen = false;
        tileMenuOpen = false;
    }

    void UI_Update() {
        if(tileMenuOpen)
            sideMenu.SetActive(true);
        else
            sideMenu.SetActive(false);

        if(settingsMenuOpen)
            settingsMenu.SetActive(true);
        else
            settingsMenu.SetActive(false);

        if(loadMenuOpen)
            loadMenu.SetActive(true);
        else
            loadMenu.SetActive(false);

        if(saveMenuOpen)
            saveMenu.SetActive(true);
        else
            saveMenu.SetActive(false);

        currentSpriteName.text = currentSprite.name;

        if(setFloor) {
            tileTypeDropdown.value = 0;
            currentTileType = TileType.hc_floor;
        }
    }

    public void UI_SideMenuClick() {
        if(tileMenuOpen)
            tileMenuOpen = false;
        else
            tileMenuOpen = true;
    }

    public void UI_SetGameMode() {
        if(gameModeDropdown.value == 0)
            currentGameType = GameType.heartlight;
        else if(gameModeDropdown.value == 1)
            currentGameType = GameType.hypercycles;
        else if(gameModeDropdown.value == 2)
            currentGameType = GameType.other;
    }

    public void UI_SetTileType() {
        if(tileTypeDropdown.value == 0)
            currentTileType = TileType.hc_floor;
        else if(tileTypeDropdown.value == 1)
            currentTileType = TileType.hl_grass;
        else if(tileTypeDropdown.value == 2)
            currentTileType = TileType.player;
        else if(tileTypeDropdown.value == 3)
            currentTileType = TileType.hl_physics;
        else if(tileTypeDropdown.value == 4)
            currentTileType = TileType.normal;
    }

    public void UI_SettingsMenuClick() {
        if(saveMenuOpen)
            saveMenuOpen = false;
        if(loadMenuOpen)
            loadMenuOpen = false;

        if(settingsMenuOpen)
            settingsMenuOpen = false;
        else
            settingsMenuOpen = true;
    }

    public void UI_LoadMenu() {
        if(saveMenuOpen)
            saveMenuOpen = false;
        if(settingsMenuOpen)
            settingsMenuOpen = false;

        if(loadMenuOpen)
            loadMenuOpen = false;
        else
            loadMenuOpen = true;
    }

    public void UI_LoadLevel() {
        grid.UI_LoadPath(loadText.text);
        UI_LoadMenu();
    }

    public void UI_SaveMenu() {
        if(settingsMenuOpen)
            settingsMenuOpen = false;
        if(loadMenuOpen)
            loadMenuOpen = false;

        if(saveMenuOpen)
            saveMenuOpen = false;
        else
            saveMenuOpen = true;
    }

    public void UI_SaveLevel() {
        grid.UI_SavePath(saveText.text);
        UI_SaveMenu();
    }

    public void UI_SetActiveSprite() {
        //Debug.Log("UI Button OK!");
        if(setFloor)
            floorSprite = highlightedSprite;
        else
            currentSprite = highlightedSprite;
    }

    public void UI_SetFloorBool() {
        if(setFloor)
            setFloor = false;
        else
            setFloor = true;
    }
    public void UI_NewGrid() {
        grid.CreateNewGrid();
        UI_SettingsMenuClick();
    }

    public void UI_UpdateSettings() {
        int x, y;

        if(int.TryParse(xSize.text, out x))
            grid.gridX = x;
        if(int.TryParse(ySize.text, out y))
            grid.gridY = y;

        grid.currentGridData.size[0] = x;
        grid.currentGridData.size[1] = y;

        grid.ClearGrid();
        grid.CreateNewGrid();
    }

    public void UI_Quit() {
        Application.Quit();
    }

    public void UI_SetFloor() {
        foreach(GameObject obj in grid.gridList) {
            if(obj.GetComponent<GridEditor>().tileType == TileType.hc_floor)
                obj.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
    }
}
