using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private float sensitivity = 1f;
	private CinemachineComposer composer;

	void Start () {
		composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
	}
	
	
	void Update () {
		float vertical = Input.GetAxis("Mouse Y") * sensitivity;
		if (vertical != 0) {
			//Debug.Log("Moving mouse");
		}
		composer.m_TrackedObjectOffset.y += vertical;
		composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -5, 5);
	}
}
