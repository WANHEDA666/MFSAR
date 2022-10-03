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
            var gameControllerView = gameFactory.CrateGameControllerScreen(defaultGameView.canvas);
            gameFactory.CreatePlayer(gameControllerView, defaultGameView.playerPosition);
        }
    }
}