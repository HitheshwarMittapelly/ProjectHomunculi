using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
	
	
	[Range(0.2f,1.5f)]
	public float fireRate = 1f;
	
	public float damage = 1f;
	public GameObject spearWeapon;
	public GameObject axeWeapon;
	private WeaponTypes currentWeapon;
	private PlayerStates currentPlayerState;
	private float timer;
	private Animator animator;
	private bool canDamage;
	private enum WeaponTypes { None,Spear, Axe};
	private enum PlayerStates { Idle, Attacking};
	private void Start() {
		canDamage = true;
		currentWeapon = WeaponTypes.None;
		currentPlayerState = PlayerStates.Idle;
		animator = GetComponentInChildren<Animator>();
	}


	void Update () {
		
		
        
		if (Input.GetKey(KeyCode.Alpha1)) {
			ChangeWeapon(WeaponTypes.Spear);
		}
		if (Input.GetKey(KeyCode.Alpha2)) {
			ChangeWeapon(WeaponTypes.Axe);
		}
		timer += Time.deltaTime;
		
		
		
		
		if (timer > fireRate) {
			if (Input.GetButton("Fire1")) {
				timer = 0f;
				FireWeapon();
			}
		}
		
	}

	private void FireWeapon() {
		
		switch (currentWeapon) {
			
			case WeaponTypes.None: break;
			case WeaponTypes.Axe:

				SetAxeAttack(true);
				canDamage = true;
				break;
			case WeaponTypes.Spear:
				canDamage = true;
				SetSpearAttack(true);
				break;

		}
		
	}

	private void FireGun() {
		Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
	}
	public void SetSpearAttack(bool value) {
		animator.SetBool("spearAttack", value);
		if (value) {
			currentPlayerState = PlayerStates.Attacking;
			animator.Play("SpearAttack");
		}
		else {
			currentPlayerState = PlayerStates.Idle;
		}
	}

	public void SetAxeAttack(bool value) {
		
		if (value) {
			currentPlayerState = PlayerStates.Attacking;
			animator.Play("AxeAttack");
		}
		else {
			currentPlayerState = PlayerStates.Idle;
		}

	}
	private void ChangeWeapon(WeaponTypes weapon) {
		axeWeapon.SetActive(false);
		spearWeapon.SetActive(false);
		currentWeapon = weapon;
		switch (currentWeapon) {
			case WeaponTypes.Axe:
				axeWeapon.SetActive(true);
				break;
			case WeaponTypes.Spear:
				spearWeapon.SetActive(true);
				break;
		}
	}

	public void WeaponTriggerCallback(Collider collider) {
		if(currentPlayerState == PlayerStates.Attacking && canDamage) {
			canDamage = false;
			Debug.Log(collider.gameObject);
			collider.gameObject.GetComponent<AnimalController>().TakeDamage(10);
		}
	}
	
}
