using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyCombat : MonoBehaviour {

    CharacterStats myStats;

    [SerializeField]
    private float attackCoolDown = 0f;

    [SerializeField]
    private float attackSpeed = 1f;

    [SerializeField]
    private float attackDelay = .6f;

    public float AttackCooldown {
        get {
            return attackCoolDown;
        }

        set {
            attackCoolDown = value;
        }
    }

    public float AttackSpeed {
        get {
            return attackSpeed;
        }

        set {
            attackSpeed = value;
        }
    }

    public float AttackDelay {
        get {
            return attackDelay;
        }

        set {
            attackDelay = value;
        }
    }

    public event System.Action onAttack;


    // Use this for initialization
    private void Start() {
        myStats = GetComponent<CharacterStats>();
    }


    // Update is called once per frame
    private void Update() {
        AttackCooldown -= Time.deltaTime;
    }


    // Attack the enemy
    public void Attack(CharacterStats targetStats) {
        if (AttackCooldown <= 0f) {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null) {
                onAttack();
            }

            AttackCooldown = 1f / attackSpeed;
        }
    }


    // Do damage based on delay
    private IEnumerator DoDamage(CharacterStats stats, float delay) {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.strength.BaseValue());
    }
}
