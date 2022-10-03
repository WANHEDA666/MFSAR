using UnityEngine;

public interface IMainMenuView
{
    GameObject MainCanvas { get; }
    GameObject ARPictureCanvas { get; }
    GameObject AboutCanvas { get; }
    Camera Camera { get; }
    Sprite SoundOn { get; }
    Sprite SoundOff { get; }
}

public class IMainMenuHolder : MonoBehaviour, IMainMenuView
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject arPictureCanvas;
    [SerializeField] private GameObject aboutCanvas;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    public GameObject MainCanvas => mainCanvas;

    public GameObject ARPictureCanvas => arPictureCanvas;

    public GameObject AboutCanvas => aboutCanvas;

    public Camera Camera => cameraMain;

    public Sprite SoundOn => soundOn;

    public Sprite SoundOff => soundOff;
}