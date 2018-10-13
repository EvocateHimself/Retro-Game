using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    /*
    // Variables
    [Header("Inventory")]
    public Inventory Inventory;

    [SerializeField] private Transform[] inventorySlots;


    // Use this for initialization
    void Start() {
        //Inventory.ItemAdded += InventoryScript_ItemAdded;
        //Inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }


    // Add item to inventory slot in UI 
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e) {

        foreach (Transform slot in inventorySlots) {

            // Border... Image
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            Image quality = slot.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            // Found empty slot
            if (!image.enabled) {
                image.enabled = true;
                //image.sprite = e.data.image;
                //quality.sprite = e.Item.quality;


                //TODO: Store a reference to the item
                itemDragHandler.Item = e.Item;

                break;
            }
        }
    }


    // Add item to inventory slot in UI 
    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e) {

        foreach (Transform slot in inventorySlots) {

            // Border... Image
            Transform imageTransform = slot.GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            Image quality = slot.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            // Found empty slot
            if (itemDragHandler.Item.Equals(e.Item)) {
                image.enabled = false;
                image.sprite = null;
                quality.sprite = null;
                itemDragHandler.Item = null;
                break;
            }
        }
    }
    */
}
