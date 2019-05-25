using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	public static AIController instance;

	private static int currentlyIdleAnimals = 0;
	private static List<AnimalController> allAnimals = new List<AnimalController>();
	// Start is called before the first frame update
	void Start()
    {
		if (instance == null) {
			instance = this;
		}
		
	}

    // Update is called once per frame
    void Update()
    {
		if (allAnimals.Count > 0) {
			FindDestinationForAnimals();
		}
    }


	public static void AddAnimalToAI(GameObject i_NewAnimal) {
		allAnimals.Add(i_NewAnimal.GetComponent<AnimalController>());
	}

	public static void FindDestinationForAnimals() {
		foreach(var animal in allAnimals) {
			if (animal.animalAIState == AnimalController.AIStates.Roaming || animal.animalAIState == AnimalController.AIStates.Idle) {
				bool foundPlayer = animal.TryToFindPlayer();
				if (foundPlayer) {
					animal.MoveToPlayer();
				}
				else if (animal.animalAIState == AnimalController.AIStates.Idle) {
					animal.MoveToRandomPoint();
				}
			}
		}
	}

	public static void UpdateIdleAnimals() {
		currentlyIdleAnimals++;
		if(currentlyIdleAnimals >= 1) {
			FindDestinationForAnimals();
		}
	}
}
