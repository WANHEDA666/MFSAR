using System;
using UnityEngine;

public interface GameState
{
    void ScyllaIsFoundFunc(Vector3 position);
    void ScyllaIsCoughtFunc(EnemieComplex enemieComplex);
    void ScyllaIsLostFunc();
    event Action<Vector3> ScyllaIsFoundAction;
    event Action<EnemieComplex> ScyllaIsCoughtAction;
    event Action ScyllaIsLostAction;
}

public class GameStateImpl : GameState
{
    public event Action<Vector3> ScyllaIsFoundAction;
    public event Action<EnemieComplex> ScyllaIsCoughtAction;
    public event Action ScyllaIsLostAction;

    public void ScyllaIsCoughtFunc(EnemieComplex enemieComplex)
    {
        ScyllaIsCoughtAction.Invoke(enemieComplex);
    }

    public void ScyllaIsFoundFunc(Vector3 position)
    {
        ScyllaIsFoundAction.Invoke(position);
    }

    public void ScyllaIsLostFunc()
    {
        ScyllaIsLostAction.Invoke();
    }
}