using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {

    // Drag inventory slot
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }


    // Drop inventory slot
    public void OnEndDrag(PointerEventData eventData) {
        transform.localPosition = Vector3.zero;
    }
}
