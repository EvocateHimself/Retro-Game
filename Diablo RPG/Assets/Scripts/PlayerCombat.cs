using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerCombat : CharacterCombat {

    Animation anim;
    CharacterStats myStats;
    PlayerStats myPlayerStats;

    // Showable/hidable UI panels
    [SerializeField]
    private GameObject inventoryPanel, mapPanel;

    // Clickable UI buttons
    [SerializeField]
    private Button primaryButton, inventoryButton, mapButton;


    [SerializeField]
    private float primaryAttackSpeed = 1f;

    [SerializeField]
    private float primaryAttackDelay = .6f;

    [SerializeField]
    private float primaryAttackMana = .6f;

    [SerializeField]
    private float secondaryAttackSpeed = 1f;

    [SerializeField]
    private float secondaryAttackDelay = .6f;

    [SerializeField]
    private float secondaryAttackMana = .6f;

    /*
    [SerializeField]
    private float attackCooldown = 0f;
    */

    public event System.Action onAttack;


    // Use this for initialization
    private void Start () {
        anim = GetComponent<Animation>();
        myStats = GetComponent<CharacterStats>();
        myPlayerStats = GetComponent<PlayerStats>();
        inventoryPanel.SetActive(false);
        mapPanel.SetActive(false);
    }


    // Check if the inventory is opened/closed and display it
    public void CheckInventory() {
        ColorBlock colorVar = inventoryButton.colors;

        if (!inventoryPanel.activeInHierarchy) {
            inventoryPanel.SetActive(true);
            colorVar.normalColor = colorVar.highlightedColor;
        } else {
            inventoryPanel.SetActive(false);
            colorVar.normalColor = Color.white;
        }
        inventoryButton.colors = colorVar;
    }


    // Check if the map is opened/closed and display it
    public void CheckMap() {
        ColorBlock colorVar = mapButton.colors;

        if (!mapPanel.activeInHierarchy) {
            mapPanel.SetActive(true);
            colorVar.normalColor = colorVar.highlightedColor;
        } else {
            mapPanel.SetActive(false);
            colorVar.normalColor = Color.white;
        }
        mapButton.colors = colorVar;
    }


    // Primary skill (slash)
    public void PrimarySkill(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            StartCoroutine(DoPrimaryDamage(targetStats, primaryAttackDelay));

            if (onAttack != null) {
                onAttack();
            }

            attackCooldown = 1f / primaryAttackSpeed;
        }
    }


    // Secondary skill (fire strike)
    public void SecondarySkill(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            StartCoroutine(DoSecondaryDamage(targetStats, secondaryAttackDelay));

            if (onAttack != null) {
                onAttack();
            }

            attackCooldown = 1f / secondaryAttackSpeed;
        }
    }


    // Activate primary skill
    private IEnumerator DoPrimaryDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        anim.Play("PrimarySkill");
        stats.TakeDamage(myStats.strength.BaseValue());
        //stats.TakeDamage(10);

        // If currentMana is less than maxMana, regenerate mana
        if (myPlayerStats.currentMana < myPlayerStats.maxMana) {
            myPlayerStats.currentMana += primaryAttackMana;
        }
        
        float calcMana = myPlayerStats.currentMana / myPlayerStats.maxMana;
        myPlayerStats.SetMana(calcMana);
    }

    // Activate primary skill
    private IEnumerator DoSecondaryDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        anim.Play("SecondarySkill");
        stats.TakeDamage(myStats.strength.BaseValue() * 1.5f);

        myPlayerStats.currentMana -= secondaryAttackMana;

        float calcMana = myPlayerStats.currentMana / myPlayerStats.maxMana;
        myPlayerStats.SetMana(calcMana);

        if (myPlayerStats.currentMana <= 0) {
            // disable skill
        }
    }

    /*
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
    }*/
}
