using Factories;
using Interfaces;
using Models;
using Views;

namespace Presenters
{
    public class GameControllerPresenter : IListenersSolver
    {
        private readonly GameControllerView gameControllerView;
        private readonly GameControllerModel gameControllerModel;
        private readonly GameFactory gameFactory;
        private readonly IBalloonCollector[] balloonCollectors;

        public GameControllerPresenter(GameControllerView gameControllerView, GameControllerModel gameControllerModel, GameFactory gameFactory, IBalloonCollector[] balloonCollectors)
        {
            this.gameControllerView = gameControllerView;
            this.gameControllerModel = gameControllerModel;
            this.gameFactory = gameFactory;
            this.balloonCollectors = balloonCollectors;
            AddListeners();
        }

        public void AddListeners()
        {
            gameControllerView.OnHomeButtonClicked += CreateMainMenuScreen;
            foreach (var balloonCollector in balloonCollectors)
            {
                balloonCollector.OnBalloonCollected += IncreaseBalloonsCount;
            }
        }

        private void IncreaseBalloonsCount()
        {
            gameControllerModel.CurrentBalloonsCount += 1;
            gameControllerView.SetCurrentBalloonsCount(gameControllerModel.CurrentBalloonsCount);
            if (gameControllerModel.CurrentBalloonsCount > gameControllerModel.BestBalloonsCount)
            {
                gameControllerView.SetBestBalloonsCount(gameControllerModel.CurrentBalloonsCount);
                gameControllerModel.BestBalloonsCount = gameControllerModel.CurrentBalloonsCount;
            }
        }

        private void CreateMainMenuScreen()
        {
            gameFactory.CreateMainMenuScreen();
        }

        public void RemoveListeners()
        {
            gameControllerView.OnHomeButtonClicked -= CreateMainMenuScreen;
            foreach (var balloonCollector in balloonCollectors)
            {
                balloonCollector.OnBalloonCollected -= IncreaseBalloonsCount;
            }
        }
    }
}