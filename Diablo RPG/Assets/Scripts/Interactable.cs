using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

    // Variables
    Transform player;

    bool isFocus = false;
    bool hasInteracted = false;

    [SerializeField] private float radius = 3f;

    public float Radius {
        get {
            return radius;
        }
        set {
            radius = value;
        }
    }

    public Transform interactionTransform;


    // Interact method that is meant to be overwritten by other classes
    public virtual void Interact() {
        Debug.Log("Interacting with " + transform.name);
    }

    // Update is called once per frame
    private void Update() {
        if(isFocus && !hasInteracted) {
            float distance = Vector3.Distance(player.position, interactionTransform.position);

            if (distance <= Radius) {
                Interact();
                hasInteracted = true;
            }
        }
    }


    public void OnFocused(Transform playerTransform) {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }


    public void OnDefocused() {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    private void OnDrawGizmosSelected() {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
