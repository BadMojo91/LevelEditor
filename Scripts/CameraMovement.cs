using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Grid grid;
    void Awake() {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }
    void Update() {
        
        float mWheelZoom;
        mWheelZoom = Input.GetAxis("Mouse ScrollWheel");
        if(Input.GetMouseButton(1))
            transform.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0)/2;

        if(transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);

        if(transform.position.x > grid.gridX)
            transform.position = new Vector3(grid.gridX, transform.position.y, transform.position.z);

        if(transform.position.y > 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if(transform.position.y < -grid.gridY)
            transform.position = new Vector3(transform.position.x, -grid.gridY, transform.position.z);

        if(mWheelZoom < 0.0f)
            Camera.main.orthographicSize += 1;

        if(mWheelZoom > 0.0f)
            Camera.main.orthographicSize -= 1;

        if(Camera.main.orthographicSize > 100)
            Camera.main.orthographicSize = 100;

        if(Camera.main.orthographicSize < 1)
            Camera.main.orthographicSize = 1;
    }
}
