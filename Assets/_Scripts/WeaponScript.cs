using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
	private WeaponManager parentWeaponManager;
	private void Start() {
		if (!parentWeaponManager){
			parentWeaponManager = GetComponentInParent<WeaponManager>();
		}
	}
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<AnimalController>()) {
			parentWeaponManager.WeaponTriggerCallback(other);
		}
	}
}
