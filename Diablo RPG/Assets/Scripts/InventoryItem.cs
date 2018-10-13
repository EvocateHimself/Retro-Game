using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInventoryItem {
    void OnPickup();
    void OnDrop();
    void OnUse();
}

// Inventory Event Arguments class
public class InventoryEventArgs : EventArgs {
    public InventoryEventArgs(IInventoryItem item) {
        Item = item;
    }

    public IInventoryItem Item;
}
