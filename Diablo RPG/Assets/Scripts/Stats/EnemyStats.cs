using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

    Item item;

    // Available loot
    [Header("Loot")]
    [SerializeField]
    private GameObject gold;
    [SerializeField]
    private GameObject healthOrb;
    [SerializeField]
    private GameObject rustyShield;

    private bool zeroHealth = false;

    public bool ZeroHealth {
        get {
            return zeroHealth;
        }

        set {
            zeroHealth = value;
        }
    }


    // Do something if the enemy is dead
    public override void Die() {
        base.Die();
        StartCoroutine(DeadAnim(2f));
    }


    // Drop loot after X seconds and destroy gameObject
    private IEnumerator DeadAnim(float delay) {
        ZeroHealth = true;
        yield return new WaitForSeconds(delay);
        int randomNumber = Random.Range(0, 3);

        switch (randomNumber) {
            case 0: Instantiate(gold, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))); break;
            case 1: Instantiate(healthOrb, transform.position + new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(0, 0, 0))); break;
            case 2: Instantiate(rustyShield, transform.position, Quaternion.Euler(new Vector3(0, 0, -90))); break;
        }

        Destroy(gameObject);
    }
}
