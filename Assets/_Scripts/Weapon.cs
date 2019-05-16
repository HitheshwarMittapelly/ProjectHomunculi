using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	
	[Range(0.2f,1.5f)]
	public float fireRate = 1f;
	
	public float damage = 1f;

	private WeaponTypes currentWeapon;
	private float timer;
	private Animator animator;
	private enum WeaponTypes { None,Spear, Axe};
	private void Start() {
		currentWeapon = WeaponTypes.None;
		animator = GetComponentInChildren<Animator>();
	}


	void Update () {
		if(animator.GetCurrentAnimatorStateInfo(0).IsName("AxeAttack")|| animator.GetCurrentAnimatorStateInfo(0).IsName("SpearAttack")) {
			return;
		}
		else {
			if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AxeAttack")) {
				SetAxeAttack(false);
			}
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("SpearAttack")) {
				SetSpearAttack(false);
			}
		}
		
        
		if (Input.GetKey(KeyCode.Alpha1)) {
			ChangeWeapon(WeaponTypes.Spear);
		}
		if (Input.GetKey(KeyCode.Alpha2)) {
			ChangeWeapon(WeaponTypes.Axe);
		}
		timer += Time.deltaTime;
		if(timer > fireRate) {
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
				break;
			case WeaponTypes.Spear:
				SetSpearAttack(true);
				break;

		}
		
	}

	private void FireGun() {
		Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
	}
	private void SetSpearAttack(bool value) {
		animator.SetBool("spearAttack", value);
	}

	private void SetAxeAttack(bool value) {
		animator.SetBool("axeAttack", value);
	}
	private void ChangeWeapon(WeaponTypes weapon) {
		currentWeapon = weapon;
	}
}
