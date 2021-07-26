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
    private CanvasController canvasController;
    private EnemieComplex enemieComplex;

    private Vector3 target;
    private bool scyllaIsFound;
    private bool scyllaIsCought;
    private Vector3 startPosition;

    public void SetEnemie(NavMeshAgent navMeshAgent, Animator animator, Vector3[] positionsForSearching, GameState gameState,
        CanvasController canvasController, EnemieComplex enemieComplex)
    {
        this.navMeshAgent = navMeshAgent;
        this.animator = animator;
        this.positionsForSearching = positionsForSearching;
        this.gameState = gameState;
        this.canvasController = canvasController;
        this.enemieComplex = enemieComplex;
        SubscribeActions();
        GetReady();
    }

    private void SubscribeActions()
    {
        gameState.ScyllaIsFoundAction += SetScyllasTarget;
        gameState.ScyllaIsCoughtAction += GameIsOver;
        gameState.ScyllaIsLostAction += SetRandomTarget;
        canvasController.Restart += Restart;
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

    private void GameIsOver(EnemieComplex enemieComplex)
    {
        scyllaIsCought = true;
        navMeshAgent.isStopped = true;
        animator.SetInteger("State", 0);
    }

    private void GetReady() {
        startPosition = gameObject.transform.position;
        animator.SetInteger("State", 1);
        navMeshAgent.isStopped = false;
        SetRandomTarget();
    }

    private void SetRandomTarget()
    {
        scyllaIsFound = false;
        target = positionsForSearching[UnityEngine.Random.Range(0, positionsForSearching.Length)];
        //navMeshAgent.SetDestination(target);
    }

    public void Update()
    {
        if (!scyllaIsFound && Math.Round(navMeshAgent.transform.position.x, 1) == Math.Round(target.x, 1) && Math.Round(navMeshAgent.transform.position.z, 1) == Math.Round(target.z, 1))
            SetRandomTarget();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!scyllaIsCought) {
            if (other.gameObject.layer == 8)
                gameState.ScyllaIsFoundFunc(other.gameObject.transform.position);
            if (Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position) < 1.4f)
                gameState.ScyllaIsCoughtFunc(enemieComplex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
            gameState.ScyllaIsLostFunc();
    }

    private void Restart()
    {
        gameObject.transform.position = startPosition;
        scyllaIsFound = scyllaIsCought = false;
        GetReady();
    }
}