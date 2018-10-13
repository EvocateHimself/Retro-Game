using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image quality;

    [SerializeField]
    private Sprite defaultQuality;

    [SerializeField]
    private Transform inventoryPanel;

    Item item;
    
    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        quality.sprite = item.quality;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        quality.sprite = defaultQuality;
    }

    public void OnItemClicked() {
        //ItemDragHandler dragHandler = gameObject.transform.Find("Item").GetComponent<ItemDragHandler>();
        UseItem();
    }

    public void UseItem() {
        if (item != null) {
            item.Use();
        }
    }

    public void OnDrop(PointerEventData eventData) {
        RectTransform invPanel = inventoryPanel as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {

            if (eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>() != null) {
                Debug.Log("Dropped object was: " + item.name);

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
