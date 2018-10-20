using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    [SerializeField]
    private float lookRadius = 10f;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
	}
	
	// Update is called once per frame
	void Update () {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius) {
            agent.SetDestination(target.position); // Move towards target

            // If within attacking distance
            if (distance <= agent.stoppingDistance) {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();

                if (targetStats != null) {
                    combat.Attack(targetStats); // Attack the target
                }
                
                FaceTarget(); // Face the target
            }
        }
	}

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
