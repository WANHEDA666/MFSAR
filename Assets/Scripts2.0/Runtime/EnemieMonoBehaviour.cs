using System;
using UnityEngine;
using UnityEngine.AI;

public interface Enemie
{

}

public class EnemieMonoBehaviour : MonoBehaviour, Enemie
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private Vector3[] positionsForSearching;
    private GameState gameState;

    private Vector3 target;
    private bool scyllaIsFound;

    public void SetEnemie(NavMeshAgent navMeshAgent, Animator animator, Vector3[] positionsForSearching, GameState gameState)
    {
        this.navMeshAgent = navMeshAgent;
        this.animator = animator;
        this.positionsForSearching = positionsForSearching;
        this.gameState = gameState;
        SubscribeActions();
        GetReady();
        SetRandomTarget();
    }

    private void SubscribeActions()
    {
        gameState.ScyllaIsFoundAction += SetScyllasTarget;
        gameState.ScyllaIsCoughtAction += GameIsOver;
        gameState.ScyllaIsLostAction += SetRandomTarget;
    }

    private void SetScyllasTarget(Vector3 position)
    {
        scyllaIsFound = true;
        target = FindTheClosestSpot(position);
        navMeshAgent.SetDestination(target);
    }

    private Vector3 FindTheClosestSpot(Vector3 ScyllaPosition)
    {
        float bestConditionFloat = 100f;
        Vector3 bestConditionVector = new Vector3();
        foreach (Vector3 pos in positionsForSearching)
        {
            if (Vector3.Distance(pos, ScyllaPosition) < bestConditionFloat)
            {
                bestConditionFloat = Math.Abs(Vector3.Distance(pos, ScyllaPosition));
                bestConditionVector = pos;
            }
        }
        return bestConditionVector;
    }

    private void GameIsOver()
    {
        navMeshAgent.isStopped = true;
        animator.SetInteger("State", 0);
    }

    private void GetReady() {
        animator.SetInteger("State", 1);
        navMeshAgent.isStopped = false;
    }

    private void SetRandomTarget()
    {
        scyllaIsFound = false;
        target = positionsForSearching[UnityEngine.Random.Range(0, positionsForSearching.Length)];
        navMeshAgent.SetDestination(target);
    }

    public void Update()
    {
        if (!scyllaIsFound && Math.Round(navMeshAgent.transform.position.x, 1) == Math.Round(target.x, 1) && Math.Round(navMeshAgent.transform.position.z, 1) == Math.Round(target.z, 1))
            SetRandomTarget();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
            gameState.ScyllaIsFoundFunc(other.gameObject.transform.position);
        if (Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position) < 1.4f)
            gameState.ScyllaIsCoughtFunc();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            gameState.ScyllaIsLostFunc();
    }
}