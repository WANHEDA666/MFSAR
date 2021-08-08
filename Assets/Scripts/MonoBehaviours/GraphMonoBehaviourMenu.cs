﻿using UnityEngine;
using UnityEngine.UI;

public sealed class GraphMonoBehaviourMenu : MonoBehaviour
{
    private void Awake()
    {
        GeneralPreferences generalPreferences = new GeneralPreferencesImpl();
        MainMenuView mainMenuView = gameObject.GetComponent<MainMenuView>();
        GameObject mainCanvas = Instantiate(mainMenuView.MainCanvas, gameObject.transform);
        mainCanvas.GetComponent<Canvas>().worldCamera = mainMenuView.Camera;
        GameObject arCanvas = Instantiate(mainMenuView.ARPictureCanvas, gameObject.transform);
        arCanvas.GetComponent<Canvas>().worldCamera = mainMenuView.Camera;
        GameObject aboutCanvas = Instantiate(mainMenuView.AboutCanvas, gameObject.transform);
        aboutCanvas.GetComponent<Canvas>().worldCamera = mainMenuView.Camera;
        MainMenu mainMenu = new MainMenuImpl(
            mainCanvas.transform.Find("ButtonARGame").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonARGame").transform.Find("ButtonARPicture").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonSimpleGame").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonAbout").GetComponent<Button>(),
            arCanvas,
            mainCanvas,
            aboutCanvas,
            arCanvas.transform.Find("PanelARPicture").transform.Find("ButtonSave").GetComponent<Button>(),
            arCanvas.transform.Find("PanelARPicture").transform.Find("ButtonMenu").GetComponent<Button>(),
            aboutCanvas.transform.Find("PanelAbout").transform.Find("ButtonMenu").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonSound").GetComponent<Button>(),
            mainMenuView.SoundOn,
            mainMenuView.SoundOff,
            mainCanvas.transform.Find("ButtonSound").transform.Find("Image").GetComponent<Image>(),
            generalPreferences,
            mainMenuView.Camera.gameObject.GetComponent<AudioSource>()
        );
    }
}