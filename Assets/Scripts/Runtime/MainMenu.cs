using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface MainMenu
{

}

public class MainMenuImpl : MainMenu
{
    private readonly Button arGameButton;
    private readonly Button arPicture;
    private readonly Button game3D;
    private readonly Button about;
    private readonly GameObject arLinkCanvas;
    private readonly GameObject mainCanvas;
    private readonly GameObject aboutCanvas;
    private readonly Button arLink;
    private readonly Button backFromARLink;
    private readonly Button backFromAboutSection;

    public MainMenuImpl(Button arGameButton, Button arPicture, Button game3D,
        Button about, GameObject arLinkCanvas, GameObject mainCanvas,
        GameObject aboutCanvas, Button arLink, Button backFromARLink,
        Button backFromAboutSection)
    {
        this.arGameButton = arGameButton;
        this.arPicture = arPicture;
        this.game3D = game3D;
        this.about = about;
        this.arLinkCanvas = arLinkCanvas;
        this.mainCanvas = mainCanvas;
        this.aboutCanvas = aboutCanvas;
        this.arLink = arLink;
        this.backFromARLink = backFromARLink;
        this.backFromAboutSection = backFromAboutSection;
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
    }

    private void LoadARScene() {
        SceneManager.LoadScene("SampleScene");
    }

    private void ShowARCanvas()
    {
        mainCanvas.SetActive(false);
        arLinkCanvas.SetActive(true);
    }

    private void Load3DScene()
    {
        SceneManager.LoadScene("SampleSceneDefault");
    }

    private void ShowAboutCanvas()
    {
        mainCanvas.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    private void LinkToSaveThePicture()
    {
        Application.OpenURL("https://drive.google.com/file/d/1olsph_7MoGcoI2n9QWj6FNEFIbxFMcvj/view?usp=sharing");
    }

    private void HideARCanvas()
    {
        mainCanvas.SetActive(true);
        arLinkCanvas.SetActive(false);
    }

    private void HideAboutCanvas()
    {
        mainCanvas.SetActive(true);
        aboutCanvas.SetActive(false);
    }
}