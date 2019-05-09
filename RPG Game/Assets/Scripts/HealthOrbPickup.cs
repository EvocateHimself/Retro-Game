using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbPickup : MonoBehaviour {

    PlayerManager playerManager;

    [SerializeField]
    private float addHealth = 10f;


    // Use this for initialization
    private void Start () {
        playerManager = PlayerManager.instance;
    }


    // Heal the player if it collides with the orb
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerStats playerStats = playerManager.player.GetComponent<PlayerStats>();
            playerStats.CurrentHealth += addHealth;
            Destroy(gameObject);
        }
    }
}
