using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class AboutView : MonoBehaviour, IListenersSolver
    {
        [SerializeField] private Button menuButton;
        public event Action OnMenuButtonClicked;

        private void Awake()
        {
            AddListeners();
        }

        public void AddListeners()
        {
            menuButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());
        }

        public void RemoveListeners()
        {
            menuButton.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}