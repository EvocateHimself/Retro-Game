using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPickup : MonoBehaviour {

    PlayerManager playerManager;

    private int addGold;

    [SerializeField]
    private int minGold;

    [SerializeField]
    private int maxGold;

    [SerializeField]
    private Text nameTag;

    public int MinGold {
        get {
            return minGold;
        }
        
        set {
            minGold = value;
        }
    }

    public int MaxGold {
        get {
            return maxGold;
        }

        set {
            maxGold = value;
        }
    }


    // Use this for initialization
    void Start () {
        playerManager = PlayerManager.instance;
        addGold = Random.Range(MinGold, MaxGold);
        nameTag.text = addGold + " Gold";
    }


    // Heal the player if it collides with the orb
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerStats playerStats = playerManager.player.GetComponent<PlayerStats>();
            playerStats.Gold += addGold;
            playerStats.goldText.text = playerStats.Gold.ToString();
            Destroy(gameObject);
        }
    }
}
