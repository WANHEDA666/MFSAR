using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
	[SerializeField] GameObject Positions;

	private NavMeshAgent meshAgent { get; set; }
	private Animator animator { get; set; }
	private Transform[] positions { get; set; }
	private Vector3 target { get; set; }

	private void Awake() {
		meshAgent = gameObject.GetComponent<NavMeshAgent>();
		animator = gameObject.GetComponent<Animator>();
		FindingEvent.EventHandler += new FindingEvent.CurrentEvent(GetScylla);
	}

	private void Start() {
		animator.SetInteger("State", 1);
		positions = new Transform[Positions.transform.childCount];
		for (int i = Positions.transform.childCount - 1; i >= 0; i--)
			positions[i] = Positions.transform.GetChild(i);
		this.SetRandomTarget();
	}

	private void Update() {
		if (Math.Round(meshAgent.transform.position.x, 1) == Math.Round(target.x, 1) && Math.Round(meshAgent.transform.position.z, 1) == Math.Round(target.z, 1)) this.SetRandomTarget();
	}

	private void SetRandomTarget() {
		target = positions[UnityEngine.Random.Range(0, positions.Length)].position;
		this.SetSpecificTarget(target);
	}

	private void SetSpecificTarget(Vector3 _target) {
		target = _target;
		meshAgent.SetDestination(target);
	}

	private void GetScylla(Vector3 ScyllaPosition) {
		float bestConditionFloat = 100f;
		Vector3 bestConditionVector = new Vector3();
		foreach (Transform pos in positions) {
			if (Vector3.Distance(pos.position, ScyllaPosition) < bestConditionFloat) {
				bestConditionFloat = Math.Abs(Vector3.Distance(pos.position, ScyllaPosition));
				bestConditionVector = pos.position;
			}
		}
		this.SetSpecificTarget(bestConditionVector);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player>() != null) {
			FindingEvent.EventHandler(other.gameObject.transform.position);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.gameObject.GetComponent<Player>() != null) {
			FindingEvent.EventHandler(other.gameObject.transform.position);
			if (Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position) < 1.4f) this.GameHasLost();
		}
	}

	private void GameHasLost() {

	}
}

public static class FindingEvent {
	public delegate void CurrentEvent(Vector3 ScyllaPosition);
	public static CurrentEvent EventHandler;
}