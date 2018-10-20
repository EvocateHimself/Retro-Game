using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    // Playerstats in inventory panel UI
    [SerializeField]
    private Text strengthText, defenseText, vitalityText, staminaText;

    public float maxMana = 100;

    public float currentMana = 0;

    [SerializeField]
    private Image manaBar;


    // Use this for initialization
    private void Start () {
        currentMana = maxMana;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}


    // Call the OnEquipmentChanged callback method
    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem) {
        if (newItem != null) {
            defense.AddModifier(newItem.defenseModifier);
            strength.AddModifier(newItem.strengthModifier);
            vitality.AddModifier(newItem.vitalityModifier);
            stamina.AddModifier(newItem.staminaModifier);
        }

        if (oldItem != null) {
            defense.RemoveModifier(oldItem.defenseModifier);
            strength.RemoveModifier(oldItem.strengthModifier);
            vitality.RemoveModifier(oldItem.vitalityModifier);
            stamina.RemoveModifier(oldItem.staminaModifier);
        }

        SetStatText();
    }


    // Set the stats in the inventory panel
    public void SetStatText() {
        int totalStrength = strength.BaseValue();
        int totalDefense = defense.BaseValue();
        int totalVitality = vitality.BaseValue();
        int totalStamina = stamina.BaseValue();

        strengthText.text = totalStrength.ToString();
        defenseText.text = totalDefense.ToString();
        vitalityText.text = totalVitality.ToString();
        staminaText.text = totalStamina.ToString();
    }


    // Set the healthbar's value to currenthealth
    public void SetMana(float myMana) {
        manaBar.fillAmount = myMana;
    }


    public override void Die() {
        base.Die();
        // Kill the player
        //PlayerManager.instance.KillPlayer();
    }
}
