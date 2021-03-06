﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

    PlayerManager playerManager;
    CharacterStats myStats;


    // Use this for initialization
    private void Start() {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }


    // Interact with the object
    public override void Interact() {
        base.Interact();
        PlayerCombat playerCombat = playerManager.player.GetComponent<PlayerCombat>();
        PlayerController playerController = playerManager.player.GetComponent<PlayerController>();

        if (playerCombat != null) {
            if (playerController.PrimaryAttack) {
                playerCombat.PrimarySkill(myStats);
            }

            else if (playerController.SecondaryAttack) {
                playerCombat.SecondarySkill(myStats);
            }
        }
    }
}
