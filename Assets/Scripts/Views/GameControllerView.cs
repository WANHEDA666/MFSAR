using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public interface IGameControllerView
    {
        Vector3 MoveVector { get; }
        Vector3 TempVector { get; }
    }

    public class GameControllerView : MonoBehaviour, IGameControllerView, IListenersSolver
    {
        [SerializeField] private Joystick joystick;
        [SerializeField] private Text bestScoreCount;
        [SerializeField] private Text currentScoreCount;
        [SerializeField] private Button homeButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject talkerPanel;
        [SerializeField] private Image talkerImage;
        [SerializeField] private Text talkerName;
        [SerializeField] private Text talkerText;
        public Vector3 MoveVector { get; private set; }
        public Vector3 TempVector { get; private set; }
        public event Action OnHomeButtonClicked;
        public event Action OnRestartButtonClicked;

        private void Awake()
        {
            AddListeners();
        }

        public void AddListeners()
        {
            homeButton.onClick.AddListener(() => OnHomeButtonClicked?.Invoke());
            restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
        }

        public void SetCurrentBalloonsCount(int count)
        {
            currentScoreCount.text = count.ToString();
        }

        public void SetBestBalloonsCount(int count)
        {
            bestScoreCount.text = count.ToString();
        }

        public void ShowGameEndScreen()
        {
            restartButton.gameObject.SetActive(true);
            joystick.Reset();
            joystick.enabled = false;
        }

        public void FixedUpdate()
        {
            var moveVector = new Vector3 {x = joystick.Horizontal, z = joystick.Vertical};
            if (moveVector != new Vector3())
            {
                var tempVector = new Vector3(moveVector.x, 0, moveVector.z);
                tempVector = tempVector.normalized * (4.5f * Time.deltaTime);
                TempVector = tempVector;
            }
            MoveVector = moveVector;
        }

        public void RemoveListeners()
        {
            homeButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}