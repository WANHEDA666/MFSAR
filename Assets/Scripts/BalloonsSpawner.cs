using System;
using System.Collections.Generic;
using UnityEngine;

public class BalloonsSpawner : MonoBehaviour
{
	[SerializeField] private Balloon Balloon;
	[SerializeField] private GameObject BaloonsPositionsHolder;

	private Transform[] BaloonsPositionsToSpawn;

	private List<GameObject> hiddenBalloons = new List<GameObject>();

	private void Awake() {
		BalloonsIncreaseEvent.EventHandler += new BalloonsIncreaseEvent.CurrentEvent(BalloonsIncrease);
		ExitEvent.EventHandler += new ExitEvent.CurrentEvent(Exit);
		BaloonsPositionsToSpawn = new Transform[BaloonsPositionsHolder.transform.childCount];
		for (int i = BaloonsPositionsHolder.transform.childCount - 1; i >= 0; i--)
			BaloonsPositionsToSpawn[i] = BaloonsPositionsHolder.transform.GetChild(i);
	}

	private void Exit() {
		BalloonsIncreaseEvent.EventHandler -= new BalloonsIncreaseEvent.CurrentEvent(BalloonsIncrease);
		ExitEvent.EventHandler -= new ExitEvent.CurrentEvent(Exit);
	}

	private void BalloonsIncrease(GameObject balloon) {
		hiddenBalloons.Add(balloon);
		if (hiddenBalloons.Count == 3) {
			hiddenBalloons[0].GetComponent<Balloon>().Reset();
			hiddenBalloons.Remove(hiddenBalloons[0]);
		}
	}

	private void Start() {
		foreach (Transform transform in BaloonsPositionsToSpawn) {
			GameObject balloon = Instantiate(Balloon.gameObject, new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z), Quaternion.identity);
			balloon.transform.SetParent(gameObject.transform.parent);
			balloon.transform.position = new Vector3(balloon.transform.position.x - 0.94f, balloon.transform.position.y, balloon.transform.position.z);
		}
	}
}

public static class BalloonsIncreaseEvent {
	public delegate void CurrentEvent(GameObject balloon);
	public static CurrentEvent EventHandler;
}