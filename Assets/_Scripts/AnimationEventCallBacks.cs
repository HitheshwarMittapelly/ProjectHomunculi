using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCallBacks : MonoBehaviour
{
	private WeaponManager weaponScript;

	private void Start() {
		weaponScript = GetComponentInParent<WeaponManager>();

	}
	public void StabbingEndEvent() {
		
		weaponScript.SetSpearAttack(false);
	}
	public void AxeEndEvent() {
	
		weaponScript.SetAxeAttack(false);
	}

}
