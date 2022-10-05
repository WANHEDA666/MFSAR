using System;
using UnityEngine;

namespace Views
{
    public interface IBalloonCollector
    {
        Action<IBalloonCollector> OnBalloonCollected { get; set; }
        void ReleaseBalloon();
    }
    
    public class BalloonView : MonoBehaviour, IBalloonCollector
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private GameObject balloon;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private CapsuleCollider capsuleCollider;
        public Action<IBalloonCollector> OnBalloonCollected { get; set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                OnBalloonCollected?.Invoke(this);
                HideBalloon();
                particle.Play();
                audioSource.Play();
            }
        }

        private void HideBalloon()
        {
            balloon.SetActive(false);
            capsuleCollider.enabled = false;
        }

        public void ReleaseBalloon()
        {
            balloon.SetActive(true);
            capsuleCollider.enabled = true;
        }
    }
}