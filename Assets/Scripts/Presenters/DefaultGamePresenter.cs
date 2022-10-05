using Factories;
using Views;

namespace Presenters
{
    public class DefaultGamePresenter
    {
        private readonly DefaultGameView defaultGameView;
        private readonly GameFactory gameFactory;

        public DefaultGamePresenter(DefaultGameView defaultGameView, GameFactory gameFactory)
        {
            this.defaultGameView = defaultGameView;
            this.gameFactory = gameFactory;
            Initialize();
        }

        private void Initialize()
        {
            var balloonsService = gameFactory.CreateBalloonsService(defaultGameView.balloonViews);
            var enemiesService = gameFactory.CreateEnemiesService(defaultGameView.enemyViews, defaultGameView.searchPositions);
            var gameControllerView = gameFactory.CrateGameControllerScreen(defaultGameView.canvas, balloonsService, enemiesService);
            gameFactory.CreatePlayer(gameControllerView, defaultGameView.playerView);
        }
    }
}