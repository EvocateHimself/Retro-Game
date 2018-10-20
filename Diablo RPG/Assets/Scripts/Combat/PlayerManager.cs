using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    #region Singleton
    public static PlayerManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of Player found!");
            return;
        }

        instance = this;
    }
    #endregion

    public GameObject player;


    // Load the scene if the player is dead
    public void KillPlayer() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
