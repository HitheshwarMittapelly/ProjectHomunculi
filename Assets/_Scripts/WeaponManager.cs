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
	private float timer;
	private Animator animator;
	private enum WeaponTypes { None,Spear, Axe};
	private void Start() {
		currentWeapon = WeaponTypes.None;
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
	public void SetSpearAttack(bool value) {
		animator.SetBool("spearAttack", value);
		if (value) {
			animator.Play("SpearAttack");
		}
	}

	public void SetAxeAttack(bool value) {
		
		if (value) {
			animator.Play("AxeAttack");
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

	}
	
}
