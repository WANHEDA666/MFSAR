using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface MainMenu : IUpdatable
{
}

public class MainMenuImpl : MainMenu
{
    private readonly Button arGameButton;
    private readonly Button arPicture;
    private readonly Button game3D;
    private readonly Button about;
    private readonly GameObject arLinkCanvas;
    private readonly GameObject aboutCanvas;
    private readonly Button arLink;
    private readonly Button backFromARLink;
    private readonly Button backFromAboutSection;
    private readonly Button sound;
    private readonly Sprite soundOn;
    private readonly Sprite soundOff;
    private readonly Image soundSprite;
    private readonly GeneralPreferences generalPreferences;
    private readonly AudioSource audioSource;
    private readonly Animator raelle;
    private readonly Animator scylla;

    private const string State = "State";

    private float currentTime;
    private bool startedDancing;

    public MainMenuImpl(Button arGameButton, Button arPicture, Button game3D,
        Button about, GameObject arLinkCanvas, GameObject aboutCanvas, Button arLink, Button backFromARLink,
        Button backFromAboutSection, Button sound, Sprite soundOn, Sprite soundOff,
        Image soundSprite, GeneralPreferences generalPreferences, AudioSource audioSource,
        Animator raelle, Animator scylla)
    {
        this.arGameButton = arGameButton;
        this.arPicture = arPicture;
        this.game3D = game3D;
        this.about = about;
        this.arLinkCanvas = arLinkCanvas;
        this.aboutCanvas = aboutCanvas;
        this.arLink = arLink;
        this.backFromARLink = backFromARLink;
        this.backFromAboutSection = backFromAboutSection;
        this.sound = sound;
        this.soundOn = soundOn;
        this.soundOff = soundOff;
        this.soundSprite = soundSprite;
        this.generalPreferences = generalPreferences;
        this.audioSource = audioSource;
        this.raelle = raelle;
        this.scylla = scylla;
        SubscribeButtons();
    }

    private void SubscribeButtons()
    {
        arGameButton.onClick.AddListener(LoadARScene);
        arPicture.onClick.AddListener(ShowARCanvas);
        game3D.onClick.AddListener(Load3DScene);
        about.onClick.AddListener(ShowAboutCanvas);
        arLink.onClick.AddListener(LinkToSaveThePicture);
        backFromARLink.onClick.AddListener(HideARCanvas);
        backFromAboutSection.onClick.AddListener(HideAboutCanvas);
        sound.onClick.AddListener(ManageSound);
        SetSound();
    }

    private void LoadARScene() {
        SceneManager.LoadScene("SampleScene");
    }

    private void ShowARCanvas()
    {
        arLinkCanvas.SetActive(true);
    }

    private void Load3DScene()
    {
        SceneManager.LoadScene("SampleSceneDefault");
    }

    private void ShowAboutCanvas()
    {
        aboutCanvas.SetActive(true);
    }

    private void LinkToSaveThePicture()
    {
        Application.OpenURL("https://drive.google.com/file/d/1olsph_7MoGcoI2n9QWj6FNEFIbxFMcvj/view?usp=sharing");
    }

    private void HideARCanvas()
    {
        arLinkCanvas.SetActive(false);
    }

    private void HideAboutCanvas()
    {
        aboutCanvas.SetActive(false);
    }

    private void ManageSound()
    {
        generalPreferences.SoundButtonState = generalPreferences.SoundButtonState == 0 ? 1 : 0;
        SetSound();
    }

    private void SetSound()
    {
        soundSprite.sprite = generalPreferences.SoundButtonState == 1 ? soundOff : soundOn;
        audioSource.enabled = generalPreferences.SoundButtonState != 1;
    }

    public void Update()
    {
        if (!startedDancing)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 60f)
            {
                startedDancing = true;
                raelle.SetInteger(State, 2);
                scylla.SetInteger(State, 2);
            }
        }
    }
}