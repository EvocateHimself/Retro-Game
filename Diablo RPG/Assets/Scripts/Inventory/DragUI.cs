using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragUI : MonoBehaviour {

    float offsetX;
    float offsetY;


    // Drag UI on hold
    public void BeginDrag() {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }


    // Drop UI on position on drag release
    public void OnDrag() {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
}
