using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]public class Tile : MonoBehaviour {
    Main main;
    Sprite sp;
    
    void Start() {
        
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { MouseEnter(); });
        trigger.triggers.Add(entry);
    }

    void Update() {

        
        sp = GetComponent<Image>().sprite;
        if(main == null)
            main = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }
    public void MouseEnter() {
        if(main != null)
            main.highlightedSprite = sp;
    }

    public void UI_SetActiveSprite() {
        Debug.Log("UI Button OK!");
        if(main.setFloor)
            main.floorSprite = main.highlightedSprite;
        else
            main.currentSprite = main.highlightedSprite;
    }


}
