using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 100;
    Vector3 newPos;
    Vector3 oldPos;
    bool moving;

    void Awake() {
        this.tag = "Player";
        GetComponent<BoxCollider>().size = new Vector3(0.9f, 0.9f, 0.9f);
    }

    void Start() {
        oldPos = transform.position;
        newPos = transform.position;

       //S Camera newCam = new Camera();
       // newCam.tag = "MainCamera";
    }

    void Update() {
        if(!moving) {
            if(Input.GetKeyDown("left"))
                newPos += Vector3.left;
            if(Input.GetKeyDown("right"))
                newPos += Vector3.right;
            if(Input.GetKeyDown("up"))
                newPos += Vector3.up;
            if(Input.GetKeyDown("down"))
                newPos += Vector3.down;
        }

        if(transform.position == newPos) {
            oldPos = transform.position;
            moving = false;
        }
        else {
            moving = true;
        }
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * speed); 
       
    }
}
