using System.Collections.Generic;
using Interfaces;
using Models;
using Presenters;
using UnityEngine;
using Views;

namespace Factories
{
    public class GameFactory : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private AboutView aboutView;
        [SerializeField] private DefaultGameView defaultGameView;
        [SerializeField] private GameControllerView gameControllerView;
        [SerializeField] private PlayerView playerView;
        [SerializeField] private EnemyView[] enemiesViews;
        private readonly List<object> views = new List<object>();
        private readonly List<object> presenters = new List<object>();

        private void RemovePreviousScreen()
        {
            for (int i = 0; i < views.Count; i++)
            {
                if (views[i] is IListenersSolver viewSolver) 
                    viewSolver.RemoveListeners();
                views[i] = null;
            }
            for (int i = 0; i < presenters.Count; i++)
            {
                if (presenters[i] is IListenersSolver viewSolver) 
                    viewSolver.RemoveListeners();
                presenters[i] = null;
            }
        }
        
        private void AddToList(object view = null, object presenter = null)
        {
            views.Add(view);
            presenters.Add(presenter);
        }

        public void CreateMainMenuScreen()
        {
            RemovePreviousScreen();
            var view = Instantiate(mainMenuView, canvas);
            var presenter = new MainMenuPresenter(view, this);
            AddToList(view, presenter);
        }

        public void CreateAboutScreen()
        {
            RemovePreviousScreen();
            var view = Instantiate(aboutView, canvas);
            var presenter = new AboutPresenter(view, this);
            AddToList(view, presenter);
        }

        public void CreateDefaultGame()
        {
            RemovePreviousScreen();
            var view = Instantiate(defaultGameView);
            var presenter = new DefaultGamePresenter(view,this);
            AddToList(view, presenter);
        }

        public IGameControllerView CrateGameControllerScreen(Transform canvas, BalloonView[] balloonViews)
        {
            var view = Instantiate(gameControllerView, canvas);
            var model = new GameControllerModel();
            var presenter = new GameControllerPresenter(view, model,this, balloonViews);
            AddToList(view, presenter);
            return view;
        }

        public void CreatePlayer(IGameControllerView gameControllerView, Transform playerPosition)
        {
            var view = Instantiate(playerView, playerPosition);
            var presenter = new PlayerPresenter(view,gameControllerView);
            AddToList(view, presenter);
        }

        public void CreateEnemies(Transform[] enemiesPositions)
        {
            for (var i = 0; i < enemiesPositions.Length; i++)
            {
                var view = Instantiate(enemiesViews[i], enemiesPositions[i]);
                var presenter = new EnemyPresenter(view);
                AddToList(view, presenter);
            }
        }
    }
}