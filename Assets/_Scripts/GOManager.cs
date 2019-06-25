using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOManager : MonoBehaviour
{
	public GameObject Tiger;
	public GameObject Elephant;
	public GameObject Leopard;

	public SpawnPointManager spawnPointManager;
	private static List<GameObject> allAnimals;
	private int totalAnimalsCap;
	private int currentAnimalCount;

	public static GOManager instance;
    // Start is called before the first frame update
    void Start()
    {
		if(instance == null) {
			instance = this;
		}
		//allAnimals = new List<GameObject>();
		//SpawnPointCalculation();
		//SpawnAnimals();
	}

	private static void SpawnPointCalculation() {
		instance.spawnPointManager.CalculateSpawnPositions();
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.J)) {
			DeleteAnAnimal();
		}
        
    }

	private static void SpawnAnimals() {
		
		foreach (var position in instance.spawnPointManager.spawnPositions) {
			//Spawn 6 animals at each position
			for (int i = 0; i < 6; i++) {
				int rand = UnityEngine.Random.Range(0, 3);
				switch (rand) {
					case 0:
						allAnimals.Add(Instantiate(instance.Tiger, position, Quaternion.identity));
						break;
					case 1:
						allAnimals.Add(Instantiate(instance.Elephant, position, Quaternion.identity));
						break;
					case 2:
						allAnimals.Add(Instantiate(instance.Leopard, position, Quaternion.identity));
						break;
				}
			}
		}
	}

	public static void RemoveAnimalFromList(GameObject i_Object) {
		allAnimals.Remove(i_Object);
		Debug.Log("Removing an animal -" + allAnimals.Count);
		if(allAnimals.Count < 20) {
			RespawnAnimals();
		}
	}

	private static void RespawnAnimals() {
		Debug.Log("Respawning animals");
		instance.spawnPointManager.CheckObjectsInColliders();
	}
	public static void SpawnAnimalsAtPosition(int count, Vector3 position) {
		for(int i=0; i < count; i++) {
			int rand = UnityEngine.Random.Range(0, 3);
			switch (rand) {
				case 0:
					allAnimals.Add(Instantiate(instance.Tiger, position, Quaternion.identity));
					break;
				case 1:
					allAnimals.Add(Instantiate(instance.Elephant, position, Quaternion.identity));
					break;
				case 2:
					allAnimals.Add(Instantiate(instance.Leopard, position, Quaternion.identity));
					break;
			}
		}
		//Debug.Log("Spawned - " + count + " animals at" + position);
	}
	private static void DeleteAnAnimal() {
		AnimalController[] animals = (AnimalController[])GameObject.FindObjectsOfType(typeof(AnimalController));
		int someAnimal = UnityEngine.Random.Range(0, animals.Length);
		animals[someAnimal].Die();
	}
}
