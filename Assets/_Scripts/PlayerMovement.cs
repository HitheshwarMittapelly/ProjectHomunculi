using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 3f;
	
	public float turnSpeed = 5f;

	private CharacterController characterController;
	private Animator animator;
	

	private void Awake() {
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		//Cursor.lockState = CursorLockMode.Locked;
	}

	

	void Update () {
		float horizontal = Input.GetAxis("Mouse X");
		float vertical = Input.GetAxis("Vertical");
		
		
		animator.SetFloat("Trigger", Mathf.Abs(vertical));
		transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
		if (vertical!=0) {
			characterController.SimpleMove(vertical * moveSpeed * transform.forward);
		}

		

	}
}
