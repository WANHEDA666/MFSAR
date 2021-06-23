using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsSpawner : MonoBehaviour
{
	[SerializeField] private Balloon Balloon;
	[SerializeField] private Transform[] BaloonsPositionsToSpawn;

	private void Start() {
		foreach (Transform transform in BaloonsPositionsToSpawn) {
			Instantiate(Balloon, transform);
		}
	}
}
