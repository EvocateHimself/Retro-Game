using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    #region Singleton
    public static InventoryManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    [SerializeField]
    private int space = 12;

    public List<Item> items = new List<Item>();


    // Add the item to inventory
    public bool Add(Item item) {
        if(!item.isDefaultItem) {
            if (items.Count >= space) {
                Debug.Log("Not enough room in your inventory.");
                return false;
            }

            items.Add(item);

            if(onItemChangedCallBack != null) {
                onItemChangedCallBack.Invoke();
            }
        }

        return true;
    }


    // Remove the item from inventory
    public void Remove(Item item) {
        items.Remove(item);

        if (onItemChangedCallBack != null) {
            onItemChangedCallBack.Invoke();
        }
    }


}
