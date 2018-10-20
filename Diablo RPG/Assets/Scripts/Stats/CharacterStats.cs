using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

    [Header("Health")]

    public float maxHealth = 100;
    public float currentHealth = 0;

    [SerializeField]
    private Image healthBar;

    // Playerstats values
    [Header("Stats")]
    public Stat strength;
    public Stat defense;
    public Stat vitality;
    public Stat stamina;


    // Initialize object variables
    private void Awake() {
        currentHealth = maxHealth;

    }


    // Decrease health upon taking damage
    public void TakeDamage(float damage) {
        damage -= defense.BaseValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        currentHealth -= damage;

        float calcHealth = currentHealth / maxHealth;
        SetHealth(calcHealth);

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0) {
            Die();
        }
    }


    // Set the healthbar's value to currenthealth
    public void SetHealth(float myHealth) {
        healthBar.fillAmount = myHealth;
    }


    // Do something if the player is dead
    public virtual void Die() {
        // Die in some way
        Debug.Log(transform.name + " died.");
    }
}
