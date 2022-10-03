using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class GraphMonoBehaviourMenu : MonoBehaviour
{
    private readonly List<object> _objects = new List<object>();
    private readonly List<object> _updatables = new List<object>();
    
    private void Awake()
    {
        GeneralPreferences generalPreferences = new GeneralPreferencesImpl();
        IMainMenuView iMainMenuView = gameObject.GetComponent<IMainMenuView>();
        GameObject mainCanvas = Instantiate(iMainMenuView.MainCanvas, gameObject.transform);
        mainCanvas.GetComponent<Canvas>().worldCamera = iMainMenuView.Camera;
        GameObject arCanvas = Instantiate(iMainMenuView.ARPictureCanvas, gameObject.transform);
        arCanvas.GetComponent<Canvas>().worldCamera = iMainMenuView.Camera;
        GameObject aboutCanvas = Instantiate(iMainMenuView.AboutCanvas, gameObject.transform);
        aboutCanvas.GetComponent<Canvas>().worldCamera = iMainMenuView.Camera;
        MainMenu mainMenu = new MainMenuImpl(
            mainCanvas.transform.Find("ButtonARGame").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonARGame").transform.Find("ButtonARPicture").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonSimpleGame").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonAbout").GetComponent<Button>(),
            arCanvas,
            aboutCanvas,
            arCanvas.transform.Find("PanelARPicture").transform.Find("ButtonSave").GetComponent<Button>(),
            arCanvas.transform.Find("PanelARPicture").transform.Find("ButtonMenu").GetComponent<Button>(),
            aboutCanvas.transform.Find("PanelAbout").transform.Find("ButtonMenu").GetComponent<Button>(),
            mainCanvas.transform.Find("ButtonSound").GetComponent<Button>(),
            iMainMenuView.SoundOn,
            iMainMenuView.SoundOff,
            mainCanvas.transform.Find("ButtonSound").transform.Find("Image").GetComponent<Image>(),
            generalPreferences,
            iMainMenuView.Camera.gameObject.GetComponent<AudioSource>(),
            mainCanvas.transform.Find("Raelle").GetComponent<Animator>(),
            mainCanvas.transform.Find("Scylla").GetComponent<Animator>()
        );
        
        _objects.Add(mainMenu);
        
        _updatables.AddRange(_objects.FindAll(objectExemplar => objectExemplar is IUpdatable));
    }

    private void Update()
    {
        foreach(IUpdatable obj in _updatables)         
            obj.Update();
    }
}