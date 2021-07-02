using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void OpenARScene() {
		SceneManager.LoadScene("SampleScene");
	}

	public void OpenDefaultScene() {
		SceneManager.LoadScene("SampleSceneDefault");
	}
}