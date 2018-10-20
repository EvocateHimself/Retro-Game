using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    private Image bar;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float currentHealth = 0f;

    // Use this for initialization
    private void Start() {
        currentHealth = maxHealth;
        //InvokeRepeating("DecreaseHealth", 0f, 2f);
    }

    private void DecreaseHealth() {
        currentHealth -= 5f;
        float calcHealth = currentHealth / maxHealth;
        SetHealth(calcHealth);
    }

    private void SetHealth(float myHealth) {
        bar.fillAmount = myHealth;
    }
}
