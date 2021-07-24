using UnityEngine;
using System.Collections;

public interface CanvasUIView
{
    GameObject Joystic { get; }
    GameObject Cup { get; }
    GameObject Balloon { get; }
    GameObject ResetPanel { get; }
}

public class CanvasUIHolder : MonoBehaviour, CanvasUIView
{
    [SerializeField] private GameObject joystic;
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject balloon;
    [SerializeField] private GameObject resetPanel;

    public GameObject Joystic => joystic;

    public GameObject Cup => cup;

    public GameObject Balloon => balloon;

    public GameObject ResetPanel => resetPanel;
}
