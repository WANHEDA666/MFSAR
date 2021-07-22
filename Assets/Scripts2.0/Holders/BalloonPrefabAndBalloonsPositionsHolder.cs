using System;
using UnityEngine;

public interface BalloonPrefabAndBalloonsPositionsView
{
    GameObject Balloon { get; }
    Vector3[] BaloonsPositions { get; }
}

public class BalloonPrefabAndBalloonsPositionsHolder : MonoBehaviour, BalloonPrefabAndBalloonsPositionsView
{
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private Vector3[] balloonsPositions;

    public GameObject Balloon => balloonPrefab;

    public Vector3[] BaloonsPositions => balloonsPositions;
}
