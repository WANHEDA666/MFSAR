using UnityEngine;

public interface Balloon
{
    void Show();
}

public class BalloonMonoBehaviour : MonoBehaviour, Balloon
{
    private BalloonsPresenter balloons;
    private ParticleSystem particle;
    private GameObject balloonItself;
    private CapsuleCollider capsule;
    private AudioSource audioSource;
    private GeneralPreferences generalPreferences;

    public void SetBalloon(BalloonsPresenter balloons, ParticleSystem particle, GameObject balloonItself, CapsuleCollider capsule, AudioSource audioSource, GeneralPreferences generalPreferences)
    {
        this.balloons = balloons;
        this.particle = particle;
        this.balloonItself = balloonItself;
        this.capsule = capsule;
        this.audioSource = audioSource;
        this.generalPreferences = generalPreferences;
    }

    public void Show()
    {
        balloonItself.SetActive(true);
        capsule.enabled = true;
    }

    private void Hide()
    {
        balloonItself.SetActive(false);
        particle.Play();
        capsule.enabled = false;
        if (generalPreferences.SoundState == 0) audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Hide();
            balloons.HideBalloon(this);
        }
    }
}
