using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]public class GridEditor : MonoBehaviour {
    Main main;
    [SerializeField]
    public Main.TileType tileType;

    SpriteRenderer spriteRend;
    [SerializeField]
    public bool isFloor;
    [SerializeField]
    public bool hasHLPhysics;
    [SerializeField]
    public bool isGrass;
    [SerializeField]
    public bool isPlayer;
    void Start() {
       // isFloor = true;
        main = GameObject.Find("Main").GetComponent<Main>();
        spriteRend = GetComponent<SpriteRenderer>();
    }


    void OnMouseOver() {
        if(main.loadMenuOpen || main.saveMenuOpen || main.settingsMenuOpen)
            return;
        else{
            if(!EventSystem.current.IsPointerOverGameObject())
                main.tileInfo.text = "TileInfo: " + main.currentLevelName + "\n" + "Type:" + tileType + "\n" + "Tile Name: " + spriteRend.sprite.name + "\n";
                main.tileInfo2.text = "Position: X:" + transform.position.x + " Y:" + transform.position.y + "\n";
                if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) {
                    if(main.currentTileType == Main.TileType.hc_floor) {
                        spriteRend.sprite = main.floorSprite;
                    }
                    else {
                        spriteRend.sprite = main.currentSprite;
                    }
                tileType = main.currentTileType;
                }
        }
    }

    void Update() {
       
        if(spriteRend.sprite != null && spriteRend.sprite.name == "Default")
            isFloor = true;
    }

}
