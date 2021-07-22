using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
	[SerializeField] private Reset reset;

	private Text BestBalloonsScore { get; set; }
	private Text CurrentBalloonsScore { get; set; }
	private int currentScore { get; set; }
	//private GirlsFaceHolder girlsFaces { get; set; }
	private Text huntersName { get; set;}
	private Text huntersSpeech { get; set; }
	private GameObject PanelReset { get; set; }
	private GameObject raelleSpeech { get; set; }

	private Dictionary<string, string> hunters = new Dictionary<string, string>() {
		{"Raelle", "I've caught you, my love" },
		{"Abigail", "So, this is necro" },
		{"Tally", "Scylla is spree" }
	};

	private void Awake() {
		BestBalloonsScore = gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		CurrentBalloonsScore = gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		//girlsFaces = gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<GirlsFaceHolder>();
		huntersName = gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		huntersSpeech = gameObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
		PanelReset = gameObject.transform.GetChild(3).gameObject;
		raelleSpeech = gameObject.transform.GetChild(4).gameObject;
		//BalloonsIncreaseEvent.EventHandler += new BalloonsIncreaseEvent.CurrentEvent(BalloonsIncrease);
		ExitEvent.EventHandler += new ExitEvent.CurrentEvent(Exit);
	}

	private void Exit() {
		//BalloonsIncreaseEvent.EventHandler -= new BalloonsIncreaseEvent.CurrentEvent(BalloonsIncrease);
		ExitEvent.EventHandler -= new ExitEvent.CurrentEvent(Exit);
	}

	private void Start() {
		this.UpdateCanvas();
	}

	private void BalloonsIncrease(GameObject balloon) {
		currentScore++;
		if (currentScore > GeneralPreferences.GetTheBestBalloonsNumber()) GeneralPreferences.SetTheBestBalloonsNumber(currentScore);
		this.UpdateCanvas();
	}

	private void UpdateCanvas() {
		BestBalloonsScore.text = GeneralPreferences.GetTheBestBalloonsNumber().ToString();
		CurrentBalloonsScore.text = currentScore.ToString();
	}

	public void SetHuntersSpeech(string name) {
		raelleSpeech.gameObject.SetActive(false);
		PanelReset.SetActive(true);
		huntersName.text = name;
		huntersSpeech.text = hunters[name];
		//girlsFaces.gameObject.GetComponent<Image>().sprite = name == "Raelle" ? girlsFaces.Raelle : name == "Abigail" ? girlsFaces.Abigail : girlsFaces.Tally;
	}

	public void SetRaelleSpeech() {
		StartCoroutine(Raelle());
	}

	private IEnumerator Raelle() {
		raelleSpeech.gameObject.SetActive(true);
		yield return new WaitForSeconds(2f);
		raelleSpeech.gameObject.SetActive(false);
	}

	public void ResetTheGame() {
		reset.ResetTheGame();
		PanelReset.SetActive(false);
		currentScore = 0;
		this.UpdateCanvas();
	}

	public void OpenTheMenu() {
		ExitEvent.EventHandler();
		SceneManager.LoadScene("MainMenu");
	}
}