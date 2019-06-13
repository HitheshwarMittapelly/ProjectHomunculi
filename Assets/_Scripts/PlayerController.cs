﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperClasses;
public class PlayerController : Health {
	
	public float baseMoveSpeed = 3f;
	public float baseStrength = 40;
	public float baseDamagePower = 10;
	public float baseJumpPower = 5;
	public float turnSpeed = 5f;

	private float currentStrength;
	public UI_Progressbar healthBar;
	public UI_Progressbar strengthBar;

	private CharacterController characterController;
	private Animator animator;
	private WeaponManager weaponManager;
	private float forwardTimer;
	private float backwardTimer;
	private float idleTimer;
	private bool forwardTimerFlag;
	private bool backwardTimerFlag;
	private EventListener animalDeathEventListener;
	private void Awake() {
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		weaponManager = GetComponent<WeaponManager>();
		
		//Cursor.lockState = CursorLockMode.Locked;
	}
	private void Start() {
		currentStrength = baseStrength;
		forwardTimer = 0;
		backwardTimer = 0;
		idleTimer = 0;
		forwardTimerFlag = false;
		backwardTimerFlag = false;
		animalDeathEventListener = new EventListener(AnimalDeathCallback);
		AnimalController.Events.Register(animalDeathEventListener);
	}

	private void AnimalDeathCallback(int eventCode, object data) {
		if((AnimalController.AnimalEvents)eventCode == AnimalController.AnimalEvents.AnimalDeath) {
			IncreaseDamagePower(2);
			Debug.Log("animal death");
		}
	}

	void Update () {
		float horizontal = Input.GetAxis("Mouse X");
		float vertical = Input.GetAxis("Vertical");
		if(vertical == 1) {
			idleTimer = 0;
			forwardTimer += Time.deltaTime;
			if(forwardTimer > 5f && !forwardTimerFlag) {
				currentStrength -= 3;
				SetCurrentStrength();
				forwardTimerFlag = true;
			}
			if(forwardTimer > 10f && forwardTimerFlag) {
				currentStrength -= 3;
				SetCurrentStrength();
				forwardTimerFlag = false;
				forwardTimer = 0;
				IncreaseMoveSpeed(1);
			}
		}else if(vertical == -1) {
			idleTimer = 0;
			backwardTimer += Time.deltaTime;
			if (backwardTimer > 5f && !backwardTimerFlag) {
				currentStrength -= 3;
				SetCurrentStrength();
				backwardTimerFlag = true;
			}
			if (backwardTimer > 10f && backwardTimerFlag) {
				currentStrength -= 3;
				SetCurrentStrength();
				backwardTimerFlag = false;
				backwardTimer = 0;
				IncreaseMoveSpeed(1);
			}
		}else if (Mathf.Approximately(vertical, 0)) {
			idleTimer += Time.deltaTime;
			if(idleTimer > 7f) {
				currentStrength += 5;
				
				SetCurrentStrength();
				idleTimer = 0;
			}
			forwardTimer = 0;
			backwardTimer = 0;
			forwardTimerFlag = false;
			backwardTimerFlag = false;
		}
		
		animator.SetFloat("Trigger", Mathf.Abs(vertical));
		transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
		if (vertical!=0) {
			characterController.SimpleMove(vertical * GetCurrentMoveSpeed() * transform.forward);
		}
		
		

	}
	public override void TakeDamage(float damage) {
		base.TakeDamage(damage);
		Debug.Log("Player taking damage - health is - " + health);
		if ((health + damage > 80 && health <= 80) || (health + damage > 50 && health <= 50)) {
			SetCurrentStrength();
		}
		healthBar.SetFillValue(health / 100f);
	}
	public override void Die() {
		base.Die();
		Destroy(gameObject);
	}
	public void IncreaseStrength(float amount) {
		baseStrength += amount;
		Mathf.Clamp(baseStrength,baseStrength,100);
		SetCurrentStrength();

	}


	public void IncreaseDamagePower(float amount) {
		baseDamagePower += amount;
		Mathf.Clamp(baseDamagePower, baseDamagePower, 20);
	}


	public void IncreaseMoveSpeed(float amount) {
		baseMoveSpeed += amount;
		Mathf.Clamp(baseMoveSpeed, baseMoveSpeed, 10);
	}


	public void IncreaseJumpPower(float amount) {
		baseJumpPower += amount;
		Mathf.Clamp(baseJumpPower, baseJumpPower, 8);
	}

	public void SetCurrentStrength() {
		Mathf.Clamp(currentStrength, currentStrength, baseStrength);
		if (health <= 100 && health > 80) {
			currentStrength = 1 * currentStrength;
		}
		else if(health <= 80 && health > 50) {
			currentStrength *= 0.75f;
		}
		else {
			currentStrength *= 0.5f;
		}
		Mathf.Clamp(currentStrength, 20, baseStrength);
		strengthBar.SetFillValue(currentStrength / baseStrength);
		
	}

	public float GetCurrentDamagePower() {
		float currentDamagePower = baseDamagePower;

		return GetStrengthFactor() * currentDamagePower;
	}

	private float GetCurrentMoveSpeed() {
		float currentMoveSpeed = baseMoveSpeed;
		return GetStrengthFactor() * currentMoveSpeed;
	}

	private float GetCurrentJumpPower() {
		float currentJumpPower = baseJumpPower;
		return GetStrengthFactor() * currentJumpPower;
	}

	private float GetStrengthFactor() {
		float factor = 1f;
		
		if (currentStrength <= 1 * baseStrength && health > 0.8f * baseStrength) {
			factor = 1f;
		}
		else if (currentStrength <= 0.8f * baseStrength && health > 0.5f * baseStrength) {
			factor = 0.75f ;
		}
		else {
			factor = 0.5f ;
		}

		return factor;
	}

	public void RunForFiveSeconds() {
		transform.Rotate(Vector3.up, 180);
		StartCoroutine("RunForSeconds");
	}

	private IEnumerator RunForSeconds() {
		
		float time = 0;
		
	
		while (time < 5) {
			time += Time.deltaTime;
			if (currentStrength == baseStrength) {
				characterController.SimpleMove(10 * transform.forward);
			}
			else {
				characterController.SimpleMove( GetCurrentMoveSpeed() * transform.forward);
			}
			animator.SetFloat("Trigger", 1);
			yield return null;
		}
	}

	public void EquipWeapon() {
		weaponManager.ChangeWeapon(WeaponManager.WeaponTypes.Axe);
	}
}
