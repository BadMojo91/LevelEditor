using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Grass : MonoBehaviour {

    void Awake() {
        this.tag = "Grass";
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }
}
