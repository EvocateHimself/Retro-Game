using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    public Transform player;
    public Equipment[] defaultItems;
    Equipment[] currentEquipment;
    GameObject instanceMesh;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryManager inventory;
    EquipmentSlot equipmentSlot;

    private void Start() {
        inventory = InventoryManager.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        EquipDefaultItems();
    }

    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = UnEquip(slotIndex);

        // An item has been equipped, trigger callback
        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        Debug.Log(newItem.equipmentPrefab.name);

        /* Equipping item */
        if (newItem.equipmentSlot == EquipmentSlot.Weapon) {
            instanceMesh = Instantiate<GameObject>(newItem.equipmentPrefab); // Instantiate equipment in the scene
            instanceMesh.transform.parent = player.transform.Find("HandRight"); // Set instantiated equipment's parent to the player
            instanceMesh.transform.localPosition = newItem.equipPosition; // Set instantiated equipment's position to the equipPosition
            instanceMesh.transform.localEulerAngles = newItem.equipRotation; // Set instantiated equipment's rotation to the equipRotation
            instanceMesh.transform.Find("NameTagCanvas").gameObject.SetActive(false); // Disable equipment's nametag UI
            instanceMesh.tag = "Equipped"; // Set equipment's tag to none (it is no longer an interactable object)
            instanceMesh.layer = 11; // Set equipment's layer to Equipment (visible to camera)
            foreach (Transform child in instanceMesh.transform) {
                child.gameObject.layer = 11;
            }
        }

        /* Equipping item */
        if (newItem.equipmentSlot == EquipmentSlot.Shield) {
            instanceMesh = Instantiate<GameObject>(newItem.equipmentPrefab); // Instantiate equipment in the scene
            instanceMesh.transform.parent = player.transform.Find("HandLeft"); // Set instantiated equipment's parent to the player
            instanceMesh.transform.localPosition = newItem.equipPosition; // Set instantiated equipment's position to the equipPosition
            instanceMesh.transform.localEulerAngles = newItem.equipRotation; // Set instantiated equipment's rotation to the equipRotation
            instanceMesh.transform.Find("NameTagCanvas").gameObject.SetActive(false); // Disable equipment's nametag UI
            instanceMesh.tag = "Equipped"; // Set equipment's tag to none (it is no longer an interactable object)
            instanceMesh.layer = 11; // Set equipment's layer to Equipment (visible to camera)
            foreach (Transform child in instanceMesh.transform) {
                child.gameObject.layer = 11;
            }
        }
    }

    public Equipment UnEquip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            /* Unequipping item */
            var clones = GameObject.FindGameObjectsWithTag("Equipped");
            foreach (var clone in clones) {
                Destroy(clone);
            }
            
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            return oldItem;
        }

        return null;
    }

    public void UnEquipAll() {
        for (int i = 0; i < currentEquipment.Length; i++) {
            UnEquip(i);
        }

        //EquipDefaultItems();
    }

    public void EquipDefaultItems() {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)) {
            UnEquipAll();
        }
    }
}
