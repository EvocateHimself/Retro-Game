#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {

    #region Singleton
    public static EquipmentManager instance;

    private void Awake() {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;
    InventoryManager inventory;
    GameObject weaponInstance, shieldInstance;

    // Player transforms
    [SerializeField] 
    private Transform player, rightHand, leftHand;

    [SerializeField]
    private Equipment[] defaultItems;

    [SerializeField]
    // Default inventory equipment icons
    private Sprite defaultQuality, defaultWeaponIcon, defaultShieldIcon; 

    [SerializeField]
    // New equipment qualities
    private Image weaponQuality, shieldQuality; 

    [SerializeField]
    // New equipment icons
    private Image weaponIcon, shieldIcon; 


    // OnEquipmentChanged callback method
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;


    // Use this for initialization
    private void Start() {
        inventory = InventoryManager.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        EquipDefaultItems();
    }


    // When the player equips an item
    public void Equip(Equipment newItem) {
        int slotIndex = (int)newItem.equipmentSlot;
        UnEquip(slotIndex);

        Equipment oldItem = null;

        // Detect equipped item
        currentEquipment[slotIndex] = newItem;

        if (newItem.equipmentSlot == EquipmentSlot.Weapon) {
            // Set weapon in equipment slot
            weaponQuality.sprite = newItem.quality;
            weaponIcon.sprite = newItem.icon;

            // Instantiate gameObject weapon on player
            weaponInstance = Instantiate<GameObject>(newItem.equipmentPrefab);
            weaponInstance.transform.parent = rightHand.transform;
            weaponInstance.transform.localPosition = newItem.equipPosition;
            weaponInstance.transform.localEulerAngles = newItem.equipRotation;
            weaponInstance.transform.Find("NameTagCanvas").gameObject.SetActive(false);
            weaponInstance.tag = "Equipped";
            weaponInstance.layer = 11;
            foreach (Transform child in weaponInstance.transform) { 
                child.gameObject.layer = 11;
            }
        } 

        else if (newItem.equipmentSlot == EquipmentSlot.Shield) {
            // Set shield in equipment slot
            shieldQuality.sprite = newItem.quality;
            shieldIcon.sprite = newItem.icon;

            // Instantiate gameObject shield on player
            shieldInstance = Instantiate<GameObject>(newItem.equipmentPrefab);
            shieldInstance.transform.parent = leftHand.transform;
            shieldInstance.transform.localPosition = newItem.equipPosition;
            shieldInstance.transform.localEulerAngles = newItem.equipRotation;
            shieldInstance.transform.Find("NameTagCanvas").gameObject.SetActive(false);
            shieldInstance.tag = "Equipped";
            shieldInstance.layer = 11;
            foreach (Transform child in shieldInstance.transform) {
                child.gameObject.layer = 11;
            }
        }

        // Call onEquipmentChanged
        if (onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
    }


    // When the player unequips an item
    public void UnEquip(int slotIndex) {
        if (currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            // Nullify equipped item
            currentEquipment[slotIndex] = null;

            // Remove weapon from equipment slot
            if (oldItem.equipmentSlot == EquipmentSlot.Weapon) {
                weaponQuality.sprite = defaultQuality;
                weaponIcon.sprite = defaultWeaponIcon;
                Destroy(weaponInstance);
            }

            // Remove shield from equipment slot
            if (oldItem.equipmentSlot == EquipmentSlot.Shield) {
                shieldQuality.sprite = defaultQuality;
                shieldIcon.sprite = defaultShieldIcon;
                Destroy(shieldInstance);
            }

            // Call onEquipmentChanged
            if (onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }


    // Equip default items on play
    public void EquipDefaultItems() {
        foreach (Equipment item in defaultItems) {
            Equip(item);
        }
    }
}
