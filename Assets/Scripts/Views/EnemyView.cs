using System;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace Views
{
    public interface IEnemy
    {
        Action<Vector3> OnPlayerSpotted { get; set; }
        Action OnPlayerLost{ get; set; }
        Action<IEnemy> OnTargetReached { get; set; }
        void SetNewTargetPosition(Vector3 target);
        Action OnPlayerCaught { get; set; }
        void StopSearching();
    }
    
    public class EnemyView : MonoBehaviour, IEnemy
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        public Action<Vector3> OnPlayerSpotted { get; set; }
        public Action OnPlayerLost { get; set; }
        public Action<IEnemy> OnTargetReached { get; set; }
        public Action OnPlayerCaught { get; set; }
        private IDisposable observable;

        private void Start()
        {
            animator.SetInteger("State", 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                OnPlayerSpotted?.Invoke(other.transform.position);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                OnPlayerLost?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == 8)
            {
                if (Vector3.Distance(other.transform.position, navMeshAgent.transform.position) < 1f)
                {
                    OnPlayerCaught?.Invoke();
                }
            }
        }

        public void SetNewTargetPosition(Vector3 target)
        {
            observable?.Dispose();
            navMeshAgent.SetDestination(target);
            observable = Observable.EveryUpdate().Subscribe(x =>
            {
                if (Vector3.Distance(target, navMeshAgent.transform.position) < 1f)
                {
                    observable.Dispose();
                    OnTargetReached?.Invoke(this);
                }
            });
        }

        public void StopSearching()
        {
            observable?.Dispose();
            navMeshAgent.isStopped = true;
            animator.SetInteger("State", 0);
        }
    }
}