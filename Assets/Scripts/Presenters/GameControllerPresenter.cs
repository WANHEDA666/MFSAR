using Factories;
using Interfaces;
using Services;
using Views;

namespace Presenters
{
    public class GameControllerPresenter : IListenersSolver
    {
        private readonly GameControllerView gameControllerView;
        private readonly GameFactory gameFactory;
        private readonly IBalloonsCollector balloonsCollector;
        private readonly IGameEndObserver gameEndObserver;

        public GameControllerPresenter(GameControllerView gameControllerView, GameFactory gameFactory, IBalloonsCollector balloonsCollector, IGameEndObserver gameEndObserver)
        {
            this.gameControllerView = gameControllerView;
            this.gameFactory = gameFactory;
            this.balloonsCollector = balloonsCollector;
            this.gameEndObserver = gameEndObserver;
            gameControllerView.SetCurrentBalloonsCount(balloonsCollector.CurrentInfo.CurrentScore);
            gameControllerView.SetBestBalloonsCount(balloonsCollector.CurrentInfo.BestScore);
            AddListeners();
        }

        public void AddListeners()
        {
            gameControllerView.OnHomeButtonClicked += CreateMainMenuScreen;
            balloonsCollector.OnCurrentScoreChanged += gameControllerView.SetCurrentBalloonsCount;
            balloonsCollector.OnBestScoreChanged += gameControllerView.SetBestBalloonsCount;
            gameEndObserver.OnGameEnded += gameControllerView.ShowGameEndScreen;
            gameControllerView.OnRestartButtonClicked += RestartGame;
        }

        private void RestartGame()
        {
            gameFactory.CreateDefaultGame();
        }

        private void CreateMainMenuScreen()
        {
            gameFactory.CreateMainMenuScreen();
        }

        public void RemoveListeners()
        {
            gameControllerView.OnHomeButtonClicked -= CreateMainMenuScreen;
            balloonsCollector.OnCurrentScoreChanged -= gameControllerView.SetCurrentBalloonsCount;
            balloonsCollector.OnBestScoreChanged -= gameControllerView.SetBestBalloonsCount;
            gameEndObserver.OnGameEnded -= gameControllerView.ShowGameEndScreen;
            gameControllerView.OnRestartButtonClicked -= RestartGame;
        }
    }
}