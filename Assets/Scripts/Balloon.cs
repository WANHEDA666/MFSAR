using UnityEngine;

public class Balloon : MonoBehaviour
{
	private ParticleSystem particle { get; set; }
	private GameObject balloonItself { get; set; }
	private CapsuleCollider capsule { get; set; }
	private AudioSource audioSource { get; set; }

	private void Awake() {
		particle = gameObject.GetComponentInChildren<ParticleSystem>();
		balloonItself = gameObject.transform.GetChild(0).gameObject;
		capsule = gameObject.GetComponent<CapsuleCollider>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<Player>() != null) {
			this.HideTheBalloon();
			BalloonsIncreaseEvent.EventHandler(gameObject);
		}
	}

	private void HideTheBalloon() {
		particle.Play();
		audioSource.Play();
		capsule.enabled = false;
		balloonItself.SetActive(false);
	}

	public void Reset() {
		capsule.enabled = true;
		balloonItself.SetActive(true);
	}
}