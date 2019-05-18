using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCallBacks : MonoBehaviour
{
	private WeaponManager weaponManager;

	private void Start() {
		weaponManager = GetComponentInParent<WeaponManager>();

	}
	public void StabbingEndEvent() {
		
		weaponManager.SetSpearAttack(false);
	}
	public void AxeEndEvent() {
	
		weaponManager.SetAxeAttack(false);
	}

}
