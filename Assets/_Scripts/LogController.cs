using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
	public Transform playerStandPosition;
	public bool playerOnLog = false;
	public float logMoveSpeed = 10f;
	private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
		startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (playerOnLog) { 
			
			transform.Translate(transform.forward * logMoveSpeed * Time.deltaTime);
			
		}
		
        
    }

	public void GoBackToInitialPosition() {
		transform.position = startPosition;
	}
	

}
