using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

    CharacterStats myStats;

    public float attackSpeed = 1f;

    public float attackCooldown = 0f;

    public float attackDelay = .6f;

    public event System.Action onAttack;


    // Use this for initialization
    private void Start() {
        myStats = GetComponent<CharacterStats>();
    }


    // Update is called once per frame
    private void Update() {
        attackCooldown -= Time.deltaTime;
    }


    // Attack the enemy
    public void Attack(CharacterStats targetStats) {
        if (attackCooldown <= 0f) {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null) {
                onAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
    }


    // Do damage based on delay
    private IEnumerator DoDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.strength.BaseValue());
        //stats.TakeDamage(10);
    }
}
