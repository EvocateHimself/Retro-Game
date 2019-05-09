using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour {

    [SerializeField]
    private Transform inventorySlotsParent;

    EquipmentSlot equipmentSlot;
    InventoryManager inventory;
    InventorySlot[] slots;


    // Use this for initialization
    private void Start () {
        inventory = InventoryManager.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
	}


    // Update the UI slots
    private void UpdateUI() {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
