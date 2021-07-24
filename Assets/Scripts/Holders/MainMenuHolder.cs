using UnityEngine;

public interface MainMenuView
{
    GameObject MainCanvas { get; }
    GameObject ARPictureCanvas { get; }
    GameObject AboutCanvas { get; }
    Camera Camera { get; }
}

public class MainMenuHolder : MonoBehaviour, MainMenuView
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject arPictureCanvas;
    [SerializeField] private GameObject aboutCanvas;
    [SerializeField] private Camera cameraMain;

    public GameObject MainCanvas => mainCanvas;

    public GameObject ARPictureCanvas => arPictureCanvas;

    public GameObject AboutCanvas => aboutCanvas;

    public Camera Camera => cameraMain;
}