using Factories;
using Interfaces;
using Views;

namespace Presenters
{
    public class AboutPresenter : IListenersSolver
    {
        private readonly AboutView aboutView;
        private readonly GameFactory gameFactory;

        public AboutPresenter(AboutView aboutView, GameFactory gameFactory)
        {
            this.aboutView = aboutView;
            this.gameFactory = gameFactory;
            AddListeners();
        }

        public void AddListeners()
        {
            aboutView.OnMenuButtonClicked += CreateMainMenuScreen;
        }

        private void CreateMainMenuScreen()
        {
            gameFactory.CreateMainMenuScreen();
        }

        public void RemoveListeners()
        {
            aboutView.OnMenuButtonClicked -= CreateMainMenuScreen;
        }
    }
}