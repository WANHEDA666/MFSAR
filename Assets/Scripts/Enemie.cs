using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemie : MonoBehaviour
{
	[SerializeField] private GameObject Positions;
	[SerializeField] private Transform plate;

	private NavMeshAgent meshAgent { get; set; }
	private Animator animator { get; set; }
	private Transform[] positions { get; set; }
	private Vector3 target { get; set; }
	private bool ScyllaIsFound { get; set; }
	private float currentTime { get; set; }
	private float raelleTime { get; set; }

	private void Awake() {
		meshAgent = gameObject.GetComponent<NavMeshAgent>();
		animator = gameObject.GetComponent<Animator>();
		FindingEvent.EventHandler += new FindingEvent.CurrentEvent(GetScylla);
		GameEndEvent.EventHandler += new GameEndEvent.CurrentEvent(GameRestart);
		ExitEvent.EventHandler += new ExitEvent.CurrentEvent(Exit);
	}

	private void Exit() {
		FindingEvent.EventHandler -= new FindingEvent.CurrentEvent(GetScylla);
		GameEndEvent.EventHandler -= new GameEndEvent.CurrentEvent(GameRestart);
		ExitEvent.EventHandler -= new ExitEvent.CurrentEvent(Exit);
	}

	private void GameRestart(string name, game_end_States restart) {
		if (restart == game_end_States.game_restart) this.GetReady();
	}

	private void Start() {
		positions = new Transform[Positions.transform.childCount];
		for (int i = Positions.transform.childCount - 1; i >= 0; i--)
			positions[i] = Positions.transform.GetChild(i);
		this.GetReady();
	}

	private void GetReady() {
		ScyllaIsFound = false;
		if (gameObject.name == "Abigail") GeneralPreferences.SetTheDestinationToAbigail(-100);
		if (gameObject.name == "Tally") GeneralPreferences.SetTheDestinationToTally(-100);
		animator.SetInteger("State", 1);
		this.SetRandomTarget();
		meshAgent.isStopped = false;
		this.Go(target);
	}

	private void Update() {
		raelleTime += Time.deltaTime;
		if (!ScyllaIsFound) {
			currentTime += Time.deltaTime;
			this.Go(target);
			if (Math.Round(meshAgent.transform.position.x, 1) == Math.Round(target.x, 1) && Math.Round(meshAgent.transform.position.z, 1) == Math.Round(target.z, 1)) {
				if (currentTime > 2) this.SetRandomTarget();
			}
		}
	}

	private void Go(Vector3 _target) {
		_target = new Vector3(_target.x, plate.position.y, _target.z);
		meshAgent.SetDestination(_target);
	}

	private void SetRandomTarget() {
		target = positions[UnityEngine.Random.Range(0, positions.Length)].position;
	}

	private void SetSpecificTarget(Vector3 _target) {
		target = _target;		
	}

	private void GetScylla(Vector3 ScyllaPosition, bool isFound) {
		if (isFound) {
			animator.SetInteger("State", 0);
			meshAgent.isStopped = true;
		}
		else {
			currentTime = 0;
			this.SetSpecificTarget(this.FindTheClosestSpot(ScyllaPosition));
		}
	}

	private Vector3 FindTheClosestSpot(Vector3 ScyllaPosition) {
		float bestConditionFloat = 100f;
		Vector3 bestConditionVector = new Vector3();
		foreach (Transform pos in positions) {
			if (Vector3.Distance(pos.position, ScyllaPosition) < bestConditionFloat) {
				bestConditionFloat = Math.Abs(Vector3.Distance(pos.position, ScyllaPosition));
				bestConditionVector = pos.position;
			}
		}
		return bestConditionVector;
	}

	private void OnTriggerEnter(Collider other) {
		if (!ScyllaIsFound && other.gameObject.GetComponent<Player>() != null) {
			FindingEvent.EventHandler(other.gameObject.transform.position, false);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (!ScyllaIsFound && other.gameObject.GetComponent<Player>() != null) {
			FindingEvent.EventHandler(other.gameObject.transform.position, false);
			if (gameObject.name == "Abigail") GeneralPreferences.SetTheDestinationToAbigail(Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position));
			if (gameObject.name == "Tally") GeneralPreferences.SetTheDestinationToTally(Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position));
			if (Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position) < 1.4f) {
				if (gameObject.name != "Raelle") {
					this.GameHasLost();
					FindingEvent.EventHandler(other.gameObject.transform.position, true);
				}
				else if (raelleTime > 3f) {
					if ((GeneralPreferences.GetTheDestinationToAbigail() > 3f || GeneralPreferences.GetTheDestinationToAbigail() == -100) && 
						(GeneralPreferences.GetTheDestinationToTally() > 3f || GeneralPreferences.GetTheDestinationToTally() == -100)) {
						int random = UnityEngine.Random.Range(0, 2);
						if (random == 1) {
							GameEndEvent.EventHandler(gameObject.name, game_end_States.raelle_talk);
						}
						else if (random == 0) {
							this.GameHasLost();
							FindingEvent.EventHandler(other.gameObject.transform.position, true);
						}
					}
					else {
						this.GameHasLost();
						FindingEvent.EventHandler(other.gameObject.transform.position, true);
					}
					raelleTime = 0;
				}
			}
		}
	}

	private void GameHasLost() {
		GameEndEvent.EventHandler(gameObject.name, game_end_States.game_lost);
	}
}

public enum game_end_States {
	game_lost = 1,
	game_restart = 2,
	raelle_talk = 3
}

public static class FindingEvent {
	public delegate void CurrentEvent(Vector3 ScyllaPosition, bool isFound);
	public static CurrentEvent EventHandler;
}