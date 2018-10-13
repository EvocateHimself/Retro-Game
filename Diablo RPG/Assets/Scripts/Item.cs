using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    [Header("Item info")]
    new public string name = "New Item";
    [TextArea]
    public string description = "Item Description";
    public Sprite icon = null;
    [Tooltip("The quality of the item (normal, magic, rare, legendary).")]
    public Sprite quality;
    public bool isDefaultItem = false;
    public GameObject equipmentPrefab;
    public Vector3 dropRotation;

    public virtual void Use() {
        // use item
        Debug.Log("Using " + name);
    }

    public virtual void RemoveFromInventory() {
        InventoryManager.instance.Remove(this);
    }
}
