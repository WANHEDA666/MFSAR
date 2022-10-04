using UnityEngine;

namespace Models
{
    public class GameControllerModel
    {
        private const string bestBalloonsCount = "BestBalloonsCount";
        private const string currentBalloonsCount = "CurrentBalloonsCount";
        
        public GameControllerModel()
        {
            ResetCurrentBalloonsCount();
        }

        private void ResetCurrentBalloonsCount()
        {
            CurrentBalloonsCount = 0;
        }
        
        public int BestBalloonsCount
        {
            get => PlayerPrefs.GetInt(bestBalloonsCount);
            set => PlayerPrefs.SetInt(bestBalloonsCount, value);
        }
        
        public int CurrentBalloonsCount
        {
            get => PlayerPrefs.GetInt(currentBalloonsCount);
            set => PlayerPrefs.SetInt(currentBalloonsCount, value);
        }
    }
}