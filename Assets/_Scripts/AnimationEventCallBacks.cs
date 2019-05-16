using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCallBacks : MonoBehaviour
{
	private Weapon weaponScript;

	private void Start() {
		weaponScript = GetComponentInParent<Weapon>();

	}
	public void StabbingEndEvent() {
		
		weaponScript.SetSpearAttack(false);
	}
	public void AxeEndEvent() {
	
		weaponScript.SetAxeAttack(false);
	}

}
