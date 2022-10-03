using Interfaces;
using UnityEngine;

namespace Views
{
    public class DefaultGameView : MonoBehaviour, IListenersSolver
    {
        public Transform canvas;
        public Transform playerPosition;
        private Camera main;

        private void Awake()
        {
            main = Camera.main;
            main.gameObject.SetActive(false);
            AddListeners();
        }
        
        public void AddListeners()
        {
            
        }

        public void RemoveListeners()
        {
            main.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}