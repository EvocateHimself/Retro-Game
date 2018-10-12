using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/*
public class PlayerControl : MonoBehaviour {

    // Variables
    NavMeshAgent agent;
    Transform target;

    // Use this for initialization
    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Left mouse click (default)
        if (Input.GetMouseButton(0)) {
            if (Physics.Raycast(ray, out hit, 100)) {
                // Keypress Shift (freeze)
                if (Input.GetKey(KeyCode.LeftShift)) {
                    agent.destination = transform.position;
                    Vector3 direction = (target.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10.0f);
                } else {
                    agent.destination = hit.point;
                }
            }
        }
    }
}
*/
