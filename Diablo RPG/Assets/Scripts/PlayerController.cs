using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Variables
    NavMeshAgent agent;
    Transform target;

    [SerializeField] private GameObject wayPoint;
    [SerializeField] private LayerMask movementMask;
    [SerializeField] private Interactable focus;


    // Use this for initialization
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        wayPoint.SetActive(false);
    }


    // Update is called once per frame
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Left mouse click (default)
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                agent.SetDestination(hit.point);
                wayPoint.transform.position = hit.point;
                RemoveFocus();
            }
        }

        // Check if we've reached the destination
            if (agent.remainingDistance > agent.stoppingDistance) {
                wayPoint.SetActive(true);
            } else {
                wayPoint.SetActive(false);
            }
        
        // Right mouse click
        if (Input.GetMouseButtonDown(1)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null) {
                    //wayPoint.transform.position = hit.point;
                    SetFocus(interactable);
                }
            }
        }

        if (target != null) {
            agent.SetDestination(target.position);
            FaceTarget();
        }
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
        wayPoint.transform.position = focus.transform.position;
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
