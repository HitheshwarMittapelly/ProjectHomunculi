using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
	private CharacterController controller;
	private Animator animatorController;
	private Transform playerTransform;
	private NavMeshAgent navMeshAgent;

	public float repathDistance = 3.5f;
	public int playerSearchRadius = 3;
	public int randomSearchRadius = 10;
	public LayerMask playerMask;
	public float health;
	public float speed;
	[HideInInspector]
	public AIStates animalAIState;
	private Vector3 lastPos;

	public enum AIStates { PursuingPlayer, ReadyToAttack,AttackingPlayer, Roaming, Idle};
    void Start()
    {
		animatorController = GetComponent<Animator>();
		
		navMeshAgent = GetComponent<NavMeshAgent>();
		animalAIState = AIStates.Idle;
		playerTransform = null;
		AIController.AddAnimalToAI(gameObject);
		AIController.FindDestinationForAnimals();
	}

  
    void Update()
    {
		if (navMeshAgent) {
			
			if (animalAIState == AIStates.PursuingPlayer) {
				if (!CheckIfReachedPlayer()) {
					if (Vector3.Distance(playerTransform.position, lastPos) > repathDistance) {
						lastPos = playerTransform.position;
						navMeshAgent.destination = playerTransform.position;
					};
				}
				
			}
			else if(animalAIState == AIStates.Roaming){
				CheckIfReachedDestination();
			}
			animatorController.SetFloat("Blend", navMeshAgent.velocity.magnitude);
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
	public void TakeDamage(float damageAmount) {
		health -= damageAmount;
		if(health <= 0) {
			Die();
		}
	}

	public void Die() {
		GOManager.RemoveAnimalFromList(gameObject);
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
		playerTransform = null;
		animalAIState = AIStates.ReadyToAttack;
		
		AttackPlayer();
		Debug.Log("Reached player");
		
		
	}

	public void AttackPlayer() {
		
		animalAIState = AIStates.AttackingPlayer;
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


	//public void AttackEndEvent() {

	//}
}
