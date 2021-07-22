using System;
using UnityEngine;

public class Reset : MonoBehaviour
{
	[SerializeField] private CanvasController canvasController;

	[SerializeField] private Vector3 ScyllaTransform;
	[SerializeField] private Vector3 TallyTransform;
	[SerializeField] private Vector3 RaelleTransform;
	[SerializeField] private Vector3 AbigailTransform;

	[SerializeField] private GameObject ScyllaGameObject;
	[SerializeField] private GameObject TallyGameObject;
	[SerializeField] private GameObject RaelleGameObject;
	[SerializeField] private GameObject AbigailGameObject;

	private void Awake() {
		//GameEndEvent.EventHandler += new GameEndEvent.CurrentEvent(GameHasBeenEnded);
		ExitEvent.EventHandler += new ExitEvent.CurrentEvent(Exit);
	}

	private void Exit() {
		//GameEndEvent.EventHandler -= new GameEndEvent.CurrentEvent(GameHasBeenEnded);
		ExitEvent.EventHandler -= new ExitEvent.CurrentEvent(Exit);
	}

	//private void GameHasBeenEnded(string name, game_end_States restart) {
	//	if (restart == game_end_States.game_lost) canvasController.SetHuntersSpeech(name);
	//	else if (restart == game_end_States.raelle_talk) {
	//		canvasController.SetRaelleSpeech();
	//	}
	//}

	public void ResetTheGame() {
		//GameEndEvent.EventHandler("", game_end_States.game_restart);
		ScyllaGameObject.transform.position = new Vector3(ScyllaTransform.x, ScyllaGameObject.transform.position.y, ScyllaTransform.z);
		TallyGameObject.transform.position = new Vector3(TallyTransform.x, TallyGameObject.transform.position.y, TallyTransform.z);
		RaelleGameObject.transform.position = new Vector3(RaelleTransform.x, RaelleGameObject.transform.position.y, RaelleTransform.z);
		AbigailGameObject.transform.position = new Vector3(AbigailTransform.x, AbigailGameObject.transform.position.y, AbigailTransform.z);
	}
}

public static class GameEndEvent {
	//public delegate void CurrentEvent(string name, game_end_States restart);
	//public static CurrentEvent EventHandler;
}

public static class ExitEvent {
	public delegate void CurrentEvent();
	public static CurrentEvent EventHandler;
}
