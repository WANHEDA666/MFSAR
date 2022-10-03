using UnityEngine;
using UnityEngine.AI;

namespace Views
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform enemyTransform;
    }
}