using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private Scylla Scylla;
	
	private Rigidbody playerRigidbody { get; set; }
	private Vector3 moveVector;
	private bool gameIsEnded { get; set; }

	private void Awake() {
		playerRigidbody = gameObject.GetComponent<Rigidbody>();
		GameEndEvent.EventHandler += new GameEndEvent.CurrentEvent(GameIsEnded);
		ExitEvent.EventHandler += new ExitEvent.CurrentEvent(Exit);
	}

	private void Exit() {
		GameEndEvent.EventHandler -= new GameEndEvent.CurrentEvent(GameIsEnded);
		ExitEvent.EventHandler -= new ExitEvent.CurrentEvent(Exit);
	}

	private void GameIsEnded(string name, game_end_States restart) {
		if (restart == game_end_States.game_restart) gameIsEnded = false;
		else if (restart == game_end_States.game_lost) gameIsEnded = true;
	}

	private void FixedUpdate() {
		moveVector = new Vector3();
		if (!gameIsEnded) {
			moveVector.x = joystick.Horizontal;
			moveVector.z = joystick.Vertical;
			Vector3 tempVect = new Vector3(moveVector.x, 0, moveVector.z);
			tempVect = tempVect.normalized * 4f * Time.deltaTime;
			playerRigidbody.MovePosition(transform.position + tempVect);
			//playerRigidbody.velocity = new Vector3(moveVector.x * 4f, playerRigidbody.velocity.y, moveVector.z * 4f);
		}
		else {
			playerRigidbody.velocity = new Vector3();
		}
		Scylla.AnimationsSolution(moveVector);
		Scylla.SetThePosition(gameObject.transform.position);
	}

	public void SetAll() {
		playerRigidbody.useGravity = true;
		gameObject.GetComponent<CapsuleCollider>().enabled = true;
	}
}