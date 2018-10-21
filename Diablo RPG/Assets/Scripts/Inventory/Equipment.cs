using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    [Header("Equipment settings")]
    public EquipmentSlot equipmentSlot;

    // Vector3 values for instantiating object to player
    public Vector3 equipPosition, equipRotation;

    [Header("Equipment attributes")]
    public int strengthModifier = 0;
    public int defenseModifier = 0;
    public int vitalityModifier = 0;
    public int staminaModifier = 0;
    public int price = 0;

    public override void Use() {
        base.Use();
        // Equip item
        EquipmentManager.instance.Equip(this);

        // Move it to equipment slot
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Necklace, Chest, Belt, Weapon, Shoulder, Gloves, Pants, Boots, Shield }
