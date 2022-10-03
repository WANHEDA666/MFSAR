using Factories;
using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] private GameFactory gameFactory;

    private void Awake()
    {
        gameFactory.CreateMainMenuScreen();
    }
}