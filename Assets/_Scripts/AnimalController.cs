using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
	public float health;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

	public void TakeDamage(float damageAmount) {
		health -= damageAmount;
		if(health <= 0) {
			Destroy(this);
		}
	}
}
