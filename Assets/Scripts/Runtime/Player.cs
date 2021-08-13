using UnityEngine;

public interface IPlayer : IFixedUpdatable
{

}

public class PlayerImpl : IPlayer
{
    private readonly Joystick joystick;
    private readonly CharacterController characterController;
    private readonly Transform playerTransform;
    private readonly Animator animator;
    private readonly GameState gameState;
    private readonly CanvasController canvasController;

    private Vector3 startPosition;

    public PlayerImpl(Joystick joystick, CharacterController characterController, Transform playerTransform, Animator animator, GameState gameState, CanvasController canvasController)
    {
        this.joystick = joystick;
        this.characterController = characterController;
        this.playerTransform = playerTransform;
        this.animator = animator;
        this.gameState = gameState;
        this.canvasController = canvasController;
        SubscribeActions();
        GetReady();
    }

    private void GetReady()
    {
        startPosition = playerTransform.position;
    }

    private void SubscribeActions()
    {
        gameState.ScyllaIsCoughtAction += GameIsOver;
        canvasController.Restart += Restart;
    }

    private void GameIsOver(EnemieComplex enemieComplex) {
        joystick.Reset();
        joystick.enabled = false;
    }

    public void FixedUpdate()
    {
        Vector3 moveVector = new Vector3();
        moveVector.x = joystick.Horizontal;
        moveVector.z = joystick.Vertical;
        if (moveVector != new Vector3()) {
            Vector3 tempVect = new Vector3(moveVector.x, 0, moveVector.z);
            tempVect = tempVect.normalized * (4.5f * Time.deltaTime);
            characterController.Move(tempVect);
        }
        AnimationsSolution(moveVector);
    }

    public void AnimationsSolution(Vector3 moveVector)
    {
        if (moveVector.x == 0 && moveVector.z == 0) animator.SetInteger("State", 0);
        else animator.SetInteger("State", 1);

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(Vector3.forward, moveVector, 3f, 0f);
            playerTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void Restart()
    {
        joystick.enabled = true;
        playerTransform.position = startPosition;
    }
}