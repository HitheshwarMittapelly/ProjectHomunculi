using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	
	[SerializeField]
	[Range(0.2f,1.5f)]
	private float fireRate = 1f;
	[SerializeField]
	private float damage = 1f;
	

	private float timer;


	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > fireRate) {
			if (Input.GetButton("Fire1")) {
				timer = 0f;
				FireWeapon();
			}
		}
		
	}

	private void FireWeapon() {
		Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
	}
}
