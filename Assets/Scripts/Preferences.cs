using UnityEngine;

public class Preferences
{
    private const string soundState = "SoundState";
    
    public static int SoundState
    {
        get => PlayerPrefs.GetInt(soundState);
        set => PlayerPrefs.SetInt(soundState, value);
    }
}