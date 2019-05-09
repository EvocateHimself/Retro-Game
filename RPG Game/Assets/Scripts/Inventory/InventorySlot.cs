using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    Item item;

    [SerializeField]
    private Image quality;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Sprite defaultQuality;

    [SerializeField]
    private Transform inventoryPanel;


    // Add item to inventory slot
    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        quality.sprite = item.quality;
    }


    // Clear inventory slots
    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quality.sprite = defaultQuality;
    }


    // Do something with the item on click
    public void OnItemClicked() {
        UseItem();
    }


    // Use the item (equip/consume)
    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }


    // Drag to drop the item
    public void OnDrop(PointerEventData eventData) {
        RectTransform invPanel = inventoryPanel as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {

            if (eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>() != null) {

                RaycastHit hit = new RaycastHit();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000)) {
                    GameObject droppedItem = Instantiate<GameObject>(item.equipmentPrefab);
                    droppedItem.transform.position = hit.point;
                    droppedItem.transform.eulerAngles = item.dropRotation;
                }

                InventoryManager.instance.Remove(item);
            }
        }
    }
}
