using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerCombat : MonoBehaviour {

    Animation anim;
    CharacterStats myStats;
    PlayerStats myPlayerStats;

    [SerializeField]
    private float attackCooldown = 0f;

    [SerializeField]
    private Text warningText;

    [Header("Inventory")]
    [SerializeField]
    private Button inventoryButton;
    [SerializeField]
    private GameObject inventoryPanel;

    [Header("Map")]
    [SerializeField]
    private Button mapButton;
    [SerializeField]
    private GameObject mapPanel;

    [Header("Primary Skill")]
    [SerializeField]
    private float primaryAttackSpeed = 1f;
    [SerializeField]
    private float primaryAttackDelay = .6f;
    [SerializeField]
    private float manaGain = 5f;
    [SerializeField]
    private AudioSource primaryHit;

    [Header("Secondary Skill")]
    [SerializeField]
    private float secondaryAttackSpeed = 1f;
    [SerializeField]
    private float secondaryAttackDelay = .6f;
    [SerializeField]
    private float secondaryAttackMana = .6f;
    [SerializeField]
    private float secondaryAttackDamageMultiplier = 3f;
    [SerializeField]
    private AudioSource secondaryHit;
    [SerializeField]
    private AudioSource impactSound;

    public float AttackCooldown {
        get {
            return attackCooldown;
        }

        set {
            attackCooldown = value;
        }
    }

    public event System.Action onAttack;


    // Use this for initialization
    private void Start () {
        anim = GetComponent<Animation>();
        myStats = GetComponent<CharacterStats>();
        myPlayerStats = GetComponent<PlayerStats>();
        inventoryPanel.SetActive(false);
        mapPanel.SetActive(false);
        warningText.gameObject.SetActive(false);
    }


    // Update is called once per frame
    private void Update() {
        AttackCooldown -= Time.deltaTime;
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
        if (AttackCooldown <= 0f) {
            StartCoroutine(DoPrimaryDamage(targetStats, primaryAttackDelay));

            if (onAttack != null) {
                onAttack();
            }

            AttackCooldown = 1f / primaryAttackSpeed;
        }
    }


    // Secondary skill (fire strike)
    public void SecondarySkill(CharacterStats targetStats) {
        if (AttackCooldown <= 0f) {
            StartCoroutine(DoSecondaryDamage(targetStats, secondaryAttackDelay));

            if (onAttack != null) {
                onAttack();
            }

            AttackCooldown = 1f / secondaryAttackSpeed;
        }
    }


    // Show warning text
    private IEnumerator ShowWarning(float delay) {
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        warningText.gameObject.SetActive(false);
    }


    // Activate primary skill
    private IEnumerator DoPrimaryDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.strength.BaseValue());
        anim.Play("PrimarySkill");
        primaryHit.Play();
        impactSound.PlayDelayed(0.1f);

        // If currentMana is less than maxMana, regenerate mana
        if (myPlayerStats.CurrentMana < myPlayerStats.MaxMana) {
            myPlayerStats.CurrentMana += manaGain;
        }
    }


    // Activate secondary skill
    private IEnumerator DoSecondaryDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        
        if (myPlayerStats.CurrentMana >= secondaryAttackMana) {
            myPlayerStats.CurrentMana -= secondaryAttackMana;
            stats.TakeDamage(myStats.strength.BaseValue() * secondaryAttackDamageMultiplier);
            anim.Play("SecondarySkill");
            secondaryHit.Play();
            impactSound.PlayDelayed(0.15f);
        } 
        
        else {
            warningText.text = "Not enough Mana!";
            StartCoroutine(ShowWarning(2));
        }
    }
}
