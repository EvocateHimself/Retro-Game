using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Variables
    NavMeshAgent agent;
    Transform target;
    Animation anim;

    [Header("Inventory")]
    public InventoryManager inventory;

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button inventoryButton;

    [Header("Map")]
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private Button mapButton;

    [Header("Movement")]
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject wayPoint;
    [SerializeField] private LayerMask movementMask;
    [SerializeField] private Interactable focus;

    [Header("Skills")]
    [SerializeField] private Button primaryButton;

    // Use this for initialization
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        wayPoint.SetActive(false);
        inventoryPanel.SetActive(false);
        mapPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        EventSystem.current.SetSelectedGameObject(null); // Disable button hover properly * requires using UnityEngine.EventSystems;
        if (!EventSystem.current.IsPointerOverGameObject()) { // Prevents raycast from passing through UI * requires using UnityEngine.EventSystems;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Left mouse click (default move)
            if (Input.GetMouseButtonDown(0)) {
                if (Physics.Raycast(ray, out hit, 100, movementMask) && hit.transform != null) {
                    wayPoint.transform.position = hit.point;
                    agent.destination = hit.point;
                    RemoveFocus();
                }

                // If left mouse click is raycasted on an interactable gameobject, focus on the interactable
                if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Interactable" && hit.collider != null) {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();

                    if (interactable != null) {
                        SetFocus(interactable);
                    }
                }
            }

            // Keypress Shift (freeze on spot)
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                    if (hit.transform != null) {
                        wayPoint.transform.position = hit.point;
                        agent.destination = transform.position;
                        transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));

                        if (Input.GetMouseButtonDown(0)) {
                            Debug.Log("attack");
                            anim.Play("PrimarySkill");
                        }
                    }
                }
            }

            // Secondary skill
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("attack");
                anim.Play("PrimarySkill");
            }

            // Keypress I (inventory)
            if (Input.GetKeyDown(KeyCode.I)) {
                CheckInventory();
            }

            // Keypress M (map)
            if (Input.GetKeyDown(KeyCode.M)) {
                CheckMap();
            }

            // Check if we've reached the destination
            if (agent.remainingDistance > agent.stoppingDistance && wayPoint != null) {
                wayPoint.SetActive(true);
            } else {
                wayPoint.SetActive(false);
            }

            if (target != null) {
                agent.SetDestination(target.position);
                FaceTarget();
            }
        }
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


    // Focus and move towards interactable
    private void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;
            FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }


    // Stop focussing
    private void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        StopFollowingTarget();
    }


    // Keep following the interactable (eg. enemy)
    private void FollowTarget(Interactable newTarget) {
        agent.stoppingDistance = newTarget.Radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
        //wayPoint.transform.position = focus.transform.position;
    }


    // Stop following the interactable (eg. enemy)
    private void StopFollowingTarget() {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }


    // Always face the target position
    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
