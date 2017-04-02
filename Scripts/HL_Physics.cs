using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HL_Physics : MonoBehaviour {
    public bool moving;
    public float speed;
    public bool onGrass;

    public Vector3 oldPos, newPos;
    void Awake() {
        this.tag = "Physics_hl";
        
        newPos = transform.position;
        newPos = new Vector3(Mathf.Round(newPos.x), Mathf.Round(newPos.y), Mathf.Round(newPos.z));
        oldPos = newPos;
    }
    void FixedUpdate() {
        
        if(!moving) {
            oldPos = transform.position;
            bool go;
            RaycastHit downHit, lupHit, rupHit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out downHit, 1)) {

                if(downHit.collider.gameObject.tag == "Grass" || downHit.collider.gameObject.tag == "Player")
                    onGrass = true;
                else
                    onGrass = false;

                if(!onGrass && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 1) && !Physics.Raycast(transform.position + transform.TransformDirection(Vector3.left), transform.TransformDirection(Vector3.down), 1)) {
                    RaycastHit upHit, leftHit;
                    if(Physics.Raycast(transform.position + transform.TransformDirection(Vector3.left), transform.TransformDirection(Vector3.up), out lupHit, 1)){
                        if(lupHit.collider.gameObject.tag == "Physics_hl") {
                            go = false;
                            return;
                        }
                        else
                            go = true;
                    }

                    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out upHit, 1)) {
                        if(upHit.collider.gameObject.tag == "Physics_hl") {
                            go = false;
                            return;
                        }
                        else {
                            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out leftHit, 2))
                                if(leftHit.collider.tag == "Physics_hl") {
                                    go = false;
                                    return;
                                }
                                else {
                                    go = true; 
                                }
                            else {
                                go = true;
                            }
                        }           
                    }
                    else {
                        go = true;
                    }

                    if(go) {
                        newPos = oldPos + Vector3.left;
                        moving = true;
                    }
                }

                else if(!onGrass && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 1) && !Physics.Raycast(transform.position + transform.TransformDirection(Vector3.right), transform.TransformDirection(Vector3.down), 1)) {
                    RaycastHit upHit, rightHit;
                    if(Physics.Raycast(transform.position + transform.TransformDirection(Vector3.right), transform.TransformDirection(Vector3.up), out rupHit, 1)) {
                        if(rupHit.collider.gameObject.tag == "Physics_hl") {
                            go = false;
                            return;
                        }
                        else
                            go = true;
                    }

                    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out upHit, 1)) {
                        if(upHit.collider.gameObject.tag == "Physics_hl") {
                            go = false;
                            return;
                        }
                        else {
                            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out rightHit, 1))
                                if(rightHit.collider.tag == "Physics_hl") {
                                    go = false;
                                    return;
                                }
                                else {
                                    go = true;
                                }
                            else {
                                go = true;
                            }
                        }
                    }
                    else {
                        go = true;
                    }

                    if(go) {
                        newPos = oldPos + Vector3.right;
                        moving = true;
                    }
                }
                else if(!onGrass && !Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 1) && !Physics.Raycast(transform.position + transform.TransformDirection(Vector3.right), transform.TransformDirection(Vector3.down), 1)) {
                    RaycastHit upHit;
                    if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out upHit, 1)) {
                        if(upHit.collider.tag != "Physics_hl") {
                            newPos = oldPos + Vector3.right;
                            moving = true;
                        }
                    }

                }
            }
            else {
                newPos = oldPos + Vector3.down;
                moving = true;
            }
            newPos = new Vector3(Mathf.Round(newPos.x), Mathf.Round(newPos.y), Mathf.Round(newPos.z));
        }
        else {

            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
            if(transform.position == newPos) {
                moving = false;
            }
        }







    }
}
