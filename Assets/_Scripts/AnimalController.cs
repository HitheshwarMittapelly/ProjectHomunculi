using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : Health
{
	private CharacterController controller;
	private Animator animatorController;
	private Transform playerTransform;
	private NavMeshAgent navMeshAgent;

	public float repathDistance = 3.5f;
	public int playerSearchRadius = 3;
	public int randomSearchRadius = 10;
	public LayerMask playerMask;
	
	public float speed;
	[HideInInspector]
	public AIStates animalAIState;
	private Vector3 lastPos;
	private bool canDamage;
	public enum AIStates { PursuingPlayer, ReadyToAttack,AttackingPlayer, Roaming, Idle};
    void Start()
    {
		animatorController = GetComponent<Animator>();
		canDamage = true;
		navMeshAgent = GetComponent<NavMeshAgent>();
		animalAIState = AIStates.Idle;
		playerTransform = null;
		AIController.AddAnimalToAI(gameObject);
		AIController.FindDestinationForAnimals();
	}

  
    void Update()
    {
		if (navMeshAgent) {
			animatorController.SetFloat("Blend", navMeshAgent.velocity.magnitude);
			if (animalAIState == AIStates.PursuingPlayer) {
				if (!CheckIfReachedPlayer()) {
					if (Vector3.Distance(playerTransform.position, lastPos) > repathDistance) {
						lastPos = playerTransform.position;
						Debug.Log("Repathing");
						navMeshAgent.destination = playerTransform.position;
					};
				}
				
			}
			else if(animalAIState == AIStates.Roaming){
				CheckIfReachedDestination();
			}
			
		}
		
		//Vector3 vel = transform.forward * speed * Time.deltaTime;
		//controller.SimpleMove(vel);
		//transform.Translate(vel);
		
		

	}
	private void CheckIfReachedDestination() {
		if (!navMeshAgent.pathPending) {
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {
					
					OnRoamingComplete();
					
				}
			}
		}
	}
	private bool CheckIfReachedPlayer() {
		if (!navMeshAgent.pathPending) {
			if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) {

					OnReachingPlayer();
					return true;
				}
			}
		}
		return false;
	}
	

	public override void Die() {
		base.Die();
		//GOManager.RemoveAnimalFromList(gameObject);
		Destroy(gameObject);
	}

	public bool TryToFindPlayer() {
		
	
		Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, playerSearchRadius,  playerMask);
		if(hitColliders.Length > 0) {
			playerTransform = hitColliders[0].transform;
			return true;
		}
		else {
			return false;
		}
		
	}

	public void MoveToPlayer() {
		Debug.Log("Moving to Player");
		navMeshAgent.SetDestination(playerTransform.position);
		lastPos = playerTransform.position;
		animalAIState = AIStates.PursuingPlayer;
	}

	public void MoveToRandomPoint() {
		Debug.Log("Moving to Random Point");
		playerTransform = null;
		
		navMeshAgent.SetDestination(RandomNavmeshLocation());
		animalAIState = AIStates.Roaming;
	}

	public void OnReachingPlayer() {
		Debug.Log("Reached player");
		playerTransform = null;
		animalAIState = AIStates.ReadyToAttack;
		
		AttackPlayer();
		
		
		
	}

	public void AttackPlayer() {
		Debug.Log("Attacking");
		
		animatorController.SetBool("attack", true);
		animalAIState = AIStates.AttackingPlayer;
		canDamage = true;
	}
	public void OnRoamingComplete() {
		Debug.Log("Reached roam point");
		animalAIState = AIStates.Idle;
		playerTransform = null;
		AIController.UpdateIdleAnimals();
		
	}

	private Vector3 RandomNavmeshLocation() {
		Vector3 randomDirection = Random.insideUnitSphere * randomSearchRadius;
		randomDirection += transform.position;
		NavMeshHit hit;
		Vector3 finalPosition = Vector3.zero;
		if (NavMesh.SamplePosition(randomDirection, out hit, randomSearchRadius, 1)) {
			finalPosition = hit.position;
		}
		return finalPosition;
	}


	public void AnimalAttackEndEvent() {
		Debug.Log("Attack ends");
		animatorController.SetBool("attack", false);
		animalAIState = AIStates.ReadyToAttack;
		StartCoroutine("SetToIdleAfterSecs");
		canDamage = true;
	}

	private IEnumerator SetToIdleAfterSecs() {
		yield return new WaitForSeconds(2);
		animalAIState = AIStates.Idle;
		AIController.UpdateIdleAnimals();
	}

	private void OnTriggerStay(Collider other) {
		if(animalAIState == AIStates.AttackingPlayer && canDamage) {
			canDamage = false;
			if (other.GetComponent<Health>()) {
				other.GetComponent<Health>().TakeDamage(10);
			}
		}
	}

}
