using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    Transform target;
    NavMeshAgent agent;
    EnemyCombat combat;
    EnemyStats enemyStats;
    Animation anim;

    [SerializeField]
    private Transform[] moveSpots;

    private int randomSpot;
    private float waitTime;

    [SerializeField]
    private float startWaitTime = 3f;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float lookRadius = 10f;

    [SerializeField]
    private Transform enemyGFX;

    private bool isIdle = false;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isDead = false;


    // Use this for initialization
    private void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<EnemyCombat>();
        enemyStats = GetComponent<EnemyStats>();
        anim = enemyGFX.GetComponent<Animation>();

        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }
	

	// Update is called once per frame
	private void Update () {
        PlayAnim();

        // Distance to the target
        float playerDistance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (playerDistance <= lookRadius) {
            agent.SetDestination(target.position); // Move towards target
            isIdle = false;
            isWalking = true;
            isAttacking = false;

            // If within attacking distance
            if (playerDistance <= agent.stoppingDistance + 2f) {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();

                if (targetStats != null) {
                    combat.Attack(targetStats); // Attack the target
                    isIdle = false;
                    isWalking = false;
                    isAttacking = true;

                    // If enemy is dead
                    if (enemyStats.ZeroHealth) {
                        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                        isDead = true;
                        isIdle = false;
                        isWalking = false;
                        isAttacking = false;
                    }
                }
                
                FaceTarget(); // Face the target
            }
        } 
    
        else {
            agent.SetDestination(moveSpots[randomSpot].position); // Move towards target
            isIdle = false;
            isWalking = true;
            isAttacking = false;

            // Distance to the moveSpot
            float patrolDistance = Vector3.Distance(moveSpots[randomSpot].position, transform.position);

            // If distance is between enemy and destination is smaller or equal to destination, the enemy has reached its destination
            if (patrolDistance <= agent.stoppingDistance) {
                // Move to new position
                if (waitTime <= 0) {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                } 

                // Wait X amount of seconds before moving again
                else {
                    waitTime -= Time.deltaTime;
                    isIdle = true;
                    isWalking = false;
                    isAttacking = false;
                }
            }
        }
	}


    // Always face the target (player)
    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    // Play GFX animations based on bool value
    private void PlayAnim() {
        if (isWalking) {
            anim.Play("walk");
            anim["walk"].speed = 2f;
        }

        if (isAttacking) {
            anim.Play("attack1");
            anim["attack1"].speed = 0.6f;
        }

        if (isIdle) {
            anim.Play("idle");
            anim["idle"].speed = 1f;
        }

        if (isDead) {
            anim.Play("death1");
            anim["death1"].speed = 1f;
        }
    }


    // Create Gizmos around gameObject in the inspector
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
