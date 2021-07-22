using System;
using UnityEngine;

public interface GameState
{
    void ScyllaIsFoundFunc(Vector3 position);
    void ScyllaIsCoughtFunc();
    void ScyllaIsLostFunc();
    event Action<Vector3> ScyllaIsFoundAction;
    event Action ScyllaIsCoughtAction;
    event Action ScyllaIsLostAction;
}

public class GameStateImpl : GameState
{
    public event Action<Vector3> ScyllaIsFoundAction;
    public event Action ScyllaIsCoughtAction;
    public event Action ScyllaIsLostAction;

    public void ScyllaIsCoughtFunc()
    {
        ScyllaIsCoughtAction.Invoke();
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