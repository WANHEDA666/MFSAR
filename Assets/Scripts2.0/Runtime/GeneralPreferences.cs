using UnityEngine;

public class GeneralPreferences
{
    public static void SetTheBestBalloonsNumber(int value) {
        PlayerPrefs.SetInt("TheBestBalloonsNumber", value);
    }

    public static int GetTheBestBalloonsNumber() {
        return PlayerPrefs.GetInt("TheBestBalloonsNumber");
    }

    public static void SetTheDestinationToAbigail(float value) {
        PlayerPrefs.SetFloat("DestinationToAbigail", value);
    }

    public static float GetTheDestinationToAbigail() {
        return PlayerPrefs.GetFloat("DestinationToAbigail");
    }

    public static void SetTheDestinationToTally(float value) {
        PlayerPrefs.SetFloat("DestinationToTally", value);
    }

    public static float GetTheDestinationToTally() {
        return PlayerPrefs.GetFloat("DestinationToTally");
    }
}