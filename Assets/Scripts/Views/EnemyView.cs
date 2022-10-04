using UnityEngine;
using UnityEngine.AI;

namespace Views
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;

        private void Start()
        {
            navMeshAgent.SetDestination(new Vector3());
        }
    }
}