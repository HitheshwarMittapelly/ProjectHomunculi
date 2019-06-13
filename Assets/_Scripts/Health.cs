using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float health;
    

	public virtual void TakeDamage(float damage) {
		health -= damage;
		if(health <= 0) {
			Die();
		}
	}

	public virtual void Die() {

	}
}
