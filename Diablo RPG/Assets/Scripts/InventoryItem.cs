using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInventoryItem {
    string Name { get; }
    Sprite Image { get; }
    Sprite Quality { get; }
    Text NameTag { get; }
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
