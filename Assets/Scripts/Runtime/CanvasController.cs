using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface CanvasController
{
    event Action Restart;
}

public class CanvasControllerImpl : CanvasController
{
    public event Action Restart;

    private readonly GameState gameState;
    private readonly Text bestScore;
    private readonly Text balloonsScore;
    private readonly Button restart;
    private readonly Button home;
    private readonly GeneralPreferences generalPreferences;
    private readonly GameObject panelSpeaker;
    private readonly Image speakerFace;
    private readonly Text speakerSpeech;
    private readonly Text speakerName;

    private Dictionary<enemie_name, string> huntersSpeech = new Dictionary<enemie_name, string>() {
        {enemie_name.raelle, "I've caught you, my love" },
        {enemie_name.abigail, "So, this is necro" },
        {enemie_name.tally, "Scylla is spree" }
    };

    private Dictionary<enemie_name, string> huntersNames = new Dictionary<enemie_name, string>() {
        {enemie_name.raelle, "Raelle" },
        {enemie_name.abigail, "Abigail" },
        {enemie_name.tally, "Tally" }
    };

    public CanvasControllerImpl(GameState gameState, Text bestScore, Text balloonsScore, Button restart, Button home, GeneralPreferences generalPreferences,
        GameObject panelSpeaker, Image speakerFace, Text speakerSpeech, Text speakerName)
    {
        this.gameState = gameState;
        this.bestScore = bestScore;
        this.balloonsScore = balloonsScore;
        this.restart = restart;
        this.home = home;
        this.generalPreferences = generalPreferences;
        this.panelSpeaker = panelSpeaker;
        this.speakerFace = speakerFace;
        this.speakerSpeech = speakerSpeech;
        this.speakerName = speakerName;
        SubscribeButtons();
        StartUIState();
    }

    private void SubscribeButtons()
    {
        generalPreferences.BalloonsEncreased += IncreaseBalloonsCount;
        generalPreferences.BestBalloonsCountIsBeaten += RenewBestBalloonsScore;
        restart.onClick.AddListener(RestartEvent);
        home.onClick.AddListener(GoToTheMainMenu);
        gameState.ScyllaIsCoughtAction += ShowFindingPanel;
    }

    private void StartUIState()
    {
        bestScore.text = generalPreferences.BestBalloonsCount.ToString();
    }

    private void IncreaseBalloonsCount(int count) {
        balloonsScore.text = count.ToString();
    }

    private void RenewBestBalloonsScore(int count)
    {
        bestScore.text = count.ToString();
    }

    private void RestartEvent()
    {
        balloonsScore.text = 0.ToString();
        restart.gameObject.SetActive(false);
        panelSpeaker.SetActive(false);
        Restart.Invoke();
    }

    private void GoToTheMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowFindingPanel(EnemieComplex enemieComplex)
    {
        speakerFace.sprite = enemieComplex.enemieFace;
        speakerName.text = huntersNames[enemieComplex.enemieName];
        speakerSpeech.text = huntersSpeech[enemieComplex.enemieName];
        panelSpeaker.SetActive(true);
        ShowRestartButton();
    }

    private void ShowRestartButton()
    {
        restart.gameObject.SetActive(true);
    }
}