using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats {

    // Playerstats in inventory panel UI
    [SerializeField]
    private Text strengthText, defenseText, vitalityText, staminaText;

    private float currentManaValue;

    [Header("Gold")]
    [SerializeField]
    private int gold = 0;
    public Text goldText;

    [Header("Mana")]
    [SerializeField]
    private float maxMana = 100;
    [SerializeField]
    private float currentMana = 0;

    [SerializeField]
    private Image manaBar;
    [SerializeField]
    private Text manaText;

    [Header("Health Warning")]
    [SerializeField]
    private float showWarningAt = 30f;
    [SerializeField]
    private float warningSpeed = 2f;
    [SerializeField]
    private Image lowHealth;

    public float CurrentMana {
        get {
            return currentMana;
        }

        set {
            currentMana = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public float MaxMana {
        get {
            return maxMana;
        }

        set {
            maxMana = value;
        }
    }

    public int Gold {
        get {
            return gold;
        }

        set {
            gold = value;
        }
    }


    // Use this for initialization
    private void Start () {
        CurrentMana = MaxMana;
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        InvokeRepeating("RegenerateMana", 0f, 2f); // regenerate mana over 2 seconds
        InvokeRepeating("RegenerateHealth", 0f, 2f); // regenerate health over 2 seconds
    }


    // Update is called once per frame
    public override void Update() {
        HandleManabar();
        HandleHealthbar();

        if (CurrentHealth <= showWarningAt) {
            FadeBlood(true);
        }

        else {
            FadeBlood(false);
        }
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


    // Do something if the player dies
    public override void Die() {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }


    // Automatically regenerate 1 mana every X second
    private void RegenerateMana() {
        CurrentMana += 1f;
    }


    // Automatically regenerate 1 mana every X second
    private void RegenerateHealth() {
        CurrentHealth += 1f;
    }

    
    // Handles the manabar my moving it and changing color
    private void HandleManabar() {
        // Writes the current mana in the text field
        manaText.text = CurrentMana + "/" + MaxMana;

        // Maps the min and max position to the range between 0 and max mana
        currentManaValue = Map(CurrentMana, 0, MaxMana, 0, 1);

        // Sets the fillAmount of the mana to simulate reduction of mana 
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, currentManaValue, Time.deltaTime * LerpSpeed);
    }


    // Fade in/fade out blood UI for low health
    private void FadeBlood(bool fadeIn) {
        // Fade in
        if (fadeIn) {
            lowHealth.CrossFadeAlpha(1, warningSpeed, false);
        }

        // Fade out
        else {
            lowHealth.CrossFadeAlpha(0, warningSpeed, false);
        }
    }
}
