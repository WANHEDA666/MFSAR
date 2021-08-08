using System;
using UnityEngine;

public interface GeneralPreferences
{
    void BalloonsCountIncreaseFunc();
    int BestBalloonsCount { get; }
    event Action<int> BalloonsEncreased;
    event Action<int> BestBalloonsCountIsBeaten;
    void ResetBalloonsCount();
    int SoundButtonState { get; set; }
}

public class GeneralPreferencesImpl : GeneralPreferences
{
    public event Action<int> BalloonsEncreased;
    public event Action<int> BestBalloonsCountIsBeaten;

    private const string BestBalloonsCountPref = "BestBalloonsCount";
    private const string SoundState = "SoundState";

    private int balloonsCount;

    public int BestBalloonsCount {
        get => PlayerPrefs.GetInt(BestBalloonsCountPref);
        set => PlayerPrefs.SetInt(BestBalloonsCountPref, value);
    }

    public int SoundButtonState {
        get => PlayerPrefs.GetInt(SoundState);
        set => PlayerPrefs.SetInt(SoundState, value);
    }

    public GeneralPreferencesImpl()
    {
        BestBalloonsCount = PlayerPrefs.GetInt(BestBalloonsCountPref);
    }

    public void BalloonsCountIncreaseFunc()
    {
        balloonsCount += 1;
        BalloonsEncreased.Invoke(balloonsCount);
        if (balloonsCount > BestBalloonsCount)
        {
            BestBalloonsCount = balloonsCount;
            BestBalloonsCountIsBeaten.Invoke(BestBalloonsCount);
        }
    }

    public void ResetBalloonsCount()
    {
        balloonsCount = 0;
    }
}