using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : Interactable {

    public Item item;

    [SerializeField]
    private Text nameTag;


    // Use this for initialization
    private void Start() {
        nameTag.text = item.name;
    }


    // Interact with item on click
    public override void Interact() {
        base.Interact();

        PickUp();
    }


    // Pickup the item
    public void PickUp() {
        bool wasPickedUp = InventoryManager.instance.Add(item);

        if (wasPickedUp) {
            Destroy(gameObject);
        }
    }
}
