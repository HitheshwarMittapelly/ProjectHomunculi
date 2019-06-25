using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
	public List<GameObject> spawnPointObjects;
	public List<BoxCollider> boxColliders;
	public List<Vector3> spawnPositions = new List<Vector3>();
	public void CalculateSpawnPositions() {
		foreach(var obj in spawnPointObjects) {
			//Debug.Log("Spawning animal at " + obj.transform.position);
			spawnPositions.Add(obj.transform.position);
			boxColliders.Add(obj.GetComponent<BoxCollider>());
		}
	}

	public void CheckObjectsInColliders() {
		foreach (var collider in boxColliders) {
			Collider[] hitColliders = Physics.OverlapBox(collider.gameObject.transform.position, collider.size / 2, Quaternion.identity);
			int i = 0;
			int animalsFound = 0;
			//Check when there is a new collider coming into contact with the box
			while (i < hitColliders.Length) {
				if (hitColliders[i]!= null) {
					if (hitColliders[i].GetComponent<AnimalController>()) {
						animalsFound++;
					}
				}
				i++;
			}
			//Debug.Log("Animals - " + animalsFound + " in this collider" + collider.gameObject.name);
			if(animalsFound < 6) {
				GOManager.SpawnAnimalsAtPosition(6 - animalsFound, collider.transform.position);
			}
		}
	}
}
