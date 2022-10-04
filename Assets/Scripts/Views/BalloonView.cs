using System;
using UnityEngine;

namespace Views
{
    public interface IBalloonCollector
    {
        Action OnBalloonCollected { get; set; }
    }
    
    public class BalloonView : MonoBehaviour, IBalloonCollector
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private GameObject balloon;
        [SerializeField] private AudioSource audioSource;
        public Action OnBalloonCollected { get; set; }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                OnBalloonCollected?.Invoke();
                balloon.SetActive(false);
                particle.Play();
                audioSource.Play();
            }
        }
    }
}