using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    [Header("Health")]
    private float currentHealthValue;

    [SerializeField]
    private float lerpSpeed = 2f;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float currentHealth = 0f;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private Text healthText;

    public float CurrentHealth {
        get {
            return currentHealth;
        }

        set {
            currentHealth = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public float MaxHealth {
        get {
            return maxHealth;
        }

        set {
            maxHealth = value;
        }
    }

    public float LerpSpeed {
        get {
            return lerpSpeed;
        }

        set {
            lerpSpeed = value;
        }
    }

    //Stat values
    [Header("Stats")]
    public Stat strength;
    public Stat defense;
    public Stat vitality;
    public Stat stamina;


    // Initialize object variables
    private void Awake() {
        CurrentHealth = MaxHealth;
    }


    // Update is called once per frame
    public virtual void Update() {
        HandleHealthbar();
    }


    // Decrease health upon taking damage
    public void TakeDamage(float damage) {
        damage -= defense.BaseValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        CurrentHealth -= damage;

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealth <= 0) {
            Die();
        }
    }


    // Do something if the player is dead
    public virtual void Die() {
        Debug.Log(transform.name + " died.");
    }


    // Handles the healthbar my moving it and changing color
    public void HandleHealthbar() {
        // Writes the current health in the text field
        healthText.text = CurrentHealth + "/" + MaxHealth;

        // Maps the min and max position to the range between 0 and max health
        currentHealthValue = Map(CurrentHealth, 0, MaxHealth, 0, 1);

        // Sets the fillAmount of the health to simulate reduction of health 
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealthValue, Time.deltaTime * LerpSpeed);
    }


    // This method maps a range of numbers into another range
    public float Map(float x, float in_min, float in_max, float out_min, float out_max) {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
