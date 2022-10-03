using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MainMenuView : MonoBehaviour, IListenersSolver
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button soundButton;
        [SerializeField] private Image soundImage;
        [SerializeField] private Sprite soundOnSprite;
        [SerializeField] private Sprite soundOffSprite;
        [SerializeField] private Button arGameButton;
        [SerializeField] private Button arGameHelpButton;
        [SerializeField] private Button defaultGameButton;
        [SerializeField] private Button aboutButton;
        public event Action OnSoundButtonClicked;
        public event Action OnARGameButtonClicked;
        public event Action OnARGameHelpButtonClicked;
        public event Action OnDefaultGameButtonClicked;
        public event Action OnAboutButtonClicked;

        private void Awake()
        {
            AddListeners();
        }

        public void AddListeners()
        {
            soundButton.onClick.AddListener(() => OnSoundButtonClicked?.Invoke());
            arGameButton.onClick.AddListener(() => OnARGameButtonClicked?.Invoke());
            arGameHelpButton.onClick.AddListener(() => OnARGameHelpButtonClicked?.Invoke());
            defaultGameButton.onClick.AddListener(() => OnDefaultGameButtonClicked?.Invoke());
            aboutButton.onClick.AddListener(() => OnAboutButtonClicked?.Invoke());
        }

        public void ManageSound(bool isEnabled)
        {
            soundImage.sprite = isEnabled ? soundOnSprite : soundOffSprite;
            audioSource.enabled = isEnabled;
        }

        public void RemoveListeners()
        {
            soundButton.onClick.RemoveAllListeners();
            arGameButton.onClick.RemoveAllListeners();
            arGameHelpButton.onClick.RemoveAllListeners();
            defaultGameButton.onClick.RemoveAllListeners();
            aboutButton.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}