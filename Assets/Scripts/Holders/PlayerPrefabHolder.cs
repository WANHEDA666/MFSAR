using UnityEngine;

public interface IPlayerView
{
    GameObject Player { get; }
}

public class PlayerPrefabHolder : MonoBehaviour, IPlayerView
{
    [SerializeField] private GameObject playerPrefab;

    public GameObject Player => playerPrefab;
}