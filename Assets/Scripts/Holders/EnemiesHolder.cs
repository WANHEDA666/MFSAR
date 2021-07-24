using UnityEngine;

public interface EnemiesView
{
    EnemieComplex Raelle { get; }
    EnemieComplex Abigail { get; }
    EnemieComplex Tally { get; }
    Vector3[] PositionsForSearching { get; }
}

public class EnemiesHolder : MonoBehaviour, EnemiesView
{
    [SerializeField] private EnemieComplex raelle;
    [SerializeField] private EnemieComplex abigail;
    [SerializeField] private EnemieComplex tally;
    [SerializeField] private Vector3[] positionsForSearching;

    public EnemieComplex Raelle => raelle;

    public EnemieComplex Abigail => abigail;

    public EnemieComplex Tally => tally;

    public Vector3[] PositionsForSearching => positionsForSearching;
}

[System.Serializable]
public class EnemieComplex
{
    public enemie_name enemieName;
    public GameObject enemiePrefab;
    public Sprite enemieFace;
}

public enum enemie_name
{
    raelle = 1,
    abigail = 2,
    tally = 3
}