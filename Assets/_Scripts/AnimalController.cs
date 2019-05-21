using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
	private CharacterController controller;
	private Animator animatorController;
	public float health;
	public float speed;
    void Start()
    {
		animatorController = GetComponent<Animator>();
		//controller = GetComponent<CharacterController>();

	}

  
    void Update()
    {
		Vector3 vel = transform.forward * speed * Time.deltaTime;
		//controller.SimpleMove(vel);
		//transform.Translate(vel);
		animatorController.SetFloat("Blend", vel.magnitude);

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

	
}
