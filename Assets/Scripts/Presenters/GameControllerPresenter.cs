using Factories;
using Interfaces;
using Views;

namespace Presenters
{
    public class GameControllerPresenter : IListenersSolver
    {
        private readonly GameControllerView gameControllerView;
        private readonly GameFactory gameFactory;

        public GameControllerPresenter(GameControllerView gameControllerView, GameFactory gameFactory)
        {
            this.gameControllerView = gameControllerView;
            this.gameFactory = gameFactory;
            AddListeners();
        }

        public void AddListeners()
        {
            gameControllerView.OnHomeButtonClicked += CreateMainMenuScreen;
        }

        private void CreateMainMenuScreen()
        {
            gameFactory.CreateMainMenuScreen();
        }

        public void RemoveListeners()
        {
            gameControllerView.OnHomeButtonClicked -= CreateMainMenuScreen;
        }
    }
}